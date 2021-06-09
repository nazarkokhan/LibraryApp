using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
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

        public async Task<Pager<BookDto>> GetBooksAsync(int page, int items, string? search)
        {
            var totalCount = await _db.Books.CountAsync();

            var noSearch = string.IsNullOrWhiteSpace(search);

            var books = _db.Books
                .OrderBy(a => a.Id)
                .TakePage(page, items)
                .Select(b => new BookDto(
                    b.Id,
                    b.Name,
                    b.AuthorBooks.Select(ab => new GetAuthorDto(
                        ab.AuthorId,
                        ab.Author.Name)
                    ))
                );

            return noSearch ? new Pager<BookDto>(await books.ToListAsync(), totalCount)
                : new Pager<BookDto>(await books.Where(b => b.Name.Contains(search!)).ToListAsync(), totalCount);
        }

        public async Task<BookDto> GetBookAsync(int id)
        {
            return await _db.Books.Select(b => new BookDto(
                b.Id,
                b.Name,
                b.AuthorBooks.Select(ab => new GetAuthorDto(
                    ab.AuthorId,
                    ab.Author.Name)
                ).ToList())
            ).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto book)
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

            return new BookDto(
                bookEntity.Id,
                bookEntity.Name,
                bookEntity.AuthorBooks.Select(ab => new GetAuthorDto(
                    ab.AuthorId,
                    ab.Author.Name)
                )
            );
        }

        public async Task<BookDto> UpdateBookAsync(UpdateBookDto book)
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

            await _db.Entry(bookEntity)
                .Collection(b => b.AuthorBooks)
                .Query()
                .Include(ab => ab.Author)
                .LoadAsync();

            return new BookDto(
                bookEntity.Id,
                bookEntity.Name,
                bookEntity.AuthorBooks.Select(ab => new GetAuthorDto(
                    ab.AuthorId,
                    ab.Author.Name)
                )
            );
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookEntity = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            _db.Books.Remove(bookEntity);

            await _db.SaveChangesAsync();
        }
    }
}