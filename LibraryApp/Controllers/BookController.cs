using LibraryApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class BookController : ControllerBase
    {
        private readonly LibContext _db;

        public BookController(LibContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetBooksAsync()
        {
            return Ok(await _db.Books.Select(b => new GetBookDto()
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.AuthorBooks.Select(ab => new GetAuthorDto()
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            }).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookDto>> GetBookAsync([Required][Range(0, int.MaxValue)] int id)
        {
            var result = await _db.Books.Where(b => b.Id == id).Select(b => new GetBookDto()
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.AuthorBooks.Select(ab => new GetAuthorDto()
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            }).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GetBookDto>> CreateBookAsync(PostBookDto book)
        {
            var bookEntity = new Book()
            {
                Name = book.Name,
            };

            await _db.AddAsync(bookEntity);

            await _db.SaveChangesAsync();

            var abEntity = book.AuthorIds.Select(aId => new AuthorBook()
            {
                BookId = bookEntity.Id,
                AuthorId = aId
            }).ToList();

            await _db.AddRangeAsync(abEntity);

            await _db.SaveChangesAsync();

            var result = new GetBookDto()
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Authors = abEntity.Select(ab => new GetAuthorDto()
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            };

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GetBookDto>> UpdateBookAsync(PutBookDto book)
        {
            var bookEntity = await _db.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.AuthorBooks)
                    .FirstAsync();

            bookEntity.Name = book.Name;

            var abEntity = book.AuthorIds.Select(aId => new AuthorBook
            {
                AuthorId = aId,
                BookId = book.Id
            }).ToList();

            _db.AuthorBooks.RemoveRange(bookEntity.AuthorBooks);

            await _db.AuthorBooks.AddRangeAsync(abEntity);

            await _db.SaveChangesAsync();

            return new GetBookDto
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Authors = abEntity.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            };
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBookAsync([Required] [Range(0, int.MaxValue)] int id)
        {
            var bookEntity = await _db.Books.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (bookEntity == null)
            {
                return NotFound();
            }
            _db.Books.Remove(bookEntity);

            return NoContent();
        }
    }
}
