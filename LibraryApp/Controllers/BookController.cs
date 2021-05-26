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
            return Ok(await _db.Books.Select(b => new GetBookDto
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.AuthorBooks.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            }).ToListAsync()); // TODO: need to add pager
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookDto>> GetBookAsync([Required][Range(0, int.MaxValue)] int id)
        {
            var result = await _db.Books.Where(b => b.Id == id).Select(b => new GetBookDto
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.AuthorBooks.Select(ab => new GetAuthorDto
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
        public async Task<ActionResult<GetBookDto>> CreateBookAsync(CreateBookDto book)
        {
            var bookEntity = new Book
            {
                Name = book.Name,
                AuthorBooks = book.AuthorIds.Select(aId => new AuthorBook
                {
                    AuthorId = aId
                }).ToList()
            };

            await _db.Books.AddAsync(bookEntity);

            await _db.SaveChangesAsync();

            await _db.Entry(bookEntity).Collection(b => b.AuthorBooks).Query().Include(ab => ab.Author).LoadAsync();

            return Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{HttpContext.Request.Path}{bookEntity.Id}",
                new GetBookDto
                {
                    Id = bookEntity.Id,
                    Name = bookEntity.Name,
                    Authors = bookEntity.AuthorBooks.Select(ab => new GetAuthorDto
                    {
                        Id = ab.AuthorId,
                        Name = ab.Author.Name
                    })
                });
        }

        [HttpPut]
        public async Task<ActionResult<GetBookDto>> UpdateBookAsync(PutBookDto book)
        {
            var bookEntity = await _db.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.AuthorBooks)
                    .FirstAsync();

            bookEntity.Name = book.Name;
            bookEntity.AuthorBooks = book.AuthorIds.Select(aId => new AuthorBook
            {
                AuthorId = aId
            }).ToList();

            await _db.SaveChangesAsync();

            await _db.Entry(bookEntity).Collection(b => b.AuthorBooks).Query().Include(ab => ab.Author).LoadAsync();

            return Ok(new GetBookDto
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Authors = bookEntity.AuthorBooks.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookAsync([Required][Range(0, int.MaxValue)] int id)
        {
            var bookEntity = await _db.Books.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (bookEntity == null)
            {
                return NotFound();
            }

            _db.Books.Remove(bookEntity);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
