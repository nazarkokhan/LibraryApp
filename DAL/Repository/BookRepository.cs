using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibContext _db;

        public BookRepository(LibContext context)
        {
            _db = context;
        }

        public async Task<Pager<GetBookDto>> GetBooksAsync(int page, int itemsOnPage)
        {
            var totalCount = await _db.Books.CountAsync();

            var books = await _db.Books
                .Skip((page - 1) * itemsOnPage)
                .Take(itemsOnPage)
                .Select(b => new GetBookDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Authors = b.AuthorBooks.Select(ab => new GetAuthorDto
                    {
                        Id = ab.AuthorId,
                        Name = ab.Author.Name
                    })
                }).ToListAsync();

            return new Pager<GetBookDto>(books, totalCount);
        }

        public async Task<GetBookDto> GetBookAsync(int id)
        {
            var result = await _db.Books.Select(b => new GetBookDto
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.AuthorBooks.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                }).ToList()
            }).FirstOrDefaultAsync(b => b.Id == id);

            return result;
        }

        public async Task<GetBookDto> CreateBookAsync(CreateBookDto book)
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

            return new GetBookDto
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Authors = bookEntity.AuthorBooks.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            };
        }

        public async Task<GetBookDto> UpdateBookAsync(UpdateBookDto book)
        {
            var bookEntity = await _db.Books
                .Include(b => b.AuthorBooks)
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            bookEntity.Name = book.Name;

            bookEntity.AuthorBooks = book.AuthorIds.Select(aId => new AuthorBook
            {
                AuthorId = aId
            }).ToList();

            await _db.SaveChangesAsync();

            await _db.Entry(bookEntity).Collection(b => b.AuthorBooks).Query().Include(ab => ab.Author).LoadAsync();

            return new GetBookDto
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Authors = bookEntity.AuthorBooks.Select(ab => new GetAuthorDto
                {
                    Id = ab.AuthorId,
                    Name = ab.Author.Name
                })
            };
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookEntity = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            _db.Books.Remove(bookEntity);

            await _db.SaveChangesAsync();
        }
    }
}
