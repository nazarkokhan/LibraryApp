using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.EF
{
    public class DbInitializer
    {
        private readonly LibContext _db;

        private List<Book> _books;

        private List<Author> _authors;

        public DbInitializer(LibContext db)
        {
            _db = db;
        }

        public async Task InitializeDbAsync()
        {
            await _db.Database.EnsureCreatedAsync();

            await InitializeAuthorsAsync();

            await InitializeBooksAsync();

            await InitializeAuthorBooksAsync();
        }

        private async Task InitializeBooksAsync()
        {
            if (!await _db.Books.AnyAsync())
            {
                _books = new List<Book>
                {
                    new Book { Name = "Book3" },
                    new Book { Name = "Book2" },
                    new Book { Name = "Book1" }
                };
                await _db.Books.AddRangeAsync(_books);
                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeAuthorsAsync()
        {
            if (!await _db.Authors.AnyAsync())
            {
                _authors = new List<Author>
                {
                    new Author { Name = "Peter" },
                    new Author { Name = "Alice" },
                    new Author { Name = "John" }
                };
                await _db.Authors.AddRangeAsync(_authors);
                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeAuthorBooksAsync()
        {
            if (!await _db.AuthorBooks.AnyAsync())
            {
                _authors ??= new List<Author>
                {
                    new Author {Name = "Peter"},
                    new Author {Name = "Alice"},
                    new Author {Name = "John"}
                };

                _books ??= new List<Book>
                {
                    new Book {Name = "Book3"},
                    new Book {Name = "Book2"},
                    new Book {Name = "Book1"}
                };

                var authorBooks = new List<AuthorBook>
                {
                    new AuthorBook {BookId = _books[0].Id, AuthorId = _authors[0].Id},
                    new AuthorBook {BookId = _books[0].Id, AuthorId = _authors[1].Id},
                    new AuthorBook {BookId = _books[1].Id, AuthorId = _authors[1].Id},
                    new AuthorBook {BookId = _books[2].Id, AuthorId = _authors[2].Id}
                };

                //var authorBooks = new List<AuthorBook>();

                //for (var i = 0; i < _books.Count; i++)
                //{
                //    authorBooks.Add(new AuthorBook() { BookId = _books[i].Id, AuthorId = _authors[i].Id });
                //}
                //authorBooks.Add(new AuthorBook() { BookId = _books[0].Id, AuthorId = _authors[1].Id });

                await _db.AuthorBooks.AddRangeAsync(authorBooks);

                await _db.SaveChangesAsync();
            }
        }
    }
}