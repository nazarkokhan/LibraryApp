using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class DataBaseInitializer
    {
        private readonly LibContext _db;

        private readonly UserManager<User> _userManager;

        private readonly List<Book> _books;

        private readonly List<Author> _authors;

        private readonly List<RegisterDto> _users;

        public DataBaseInitializer(LibContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;

            _authors ??= new List<Author>
            {
                new() {Name = "Peter"},
                new() {Name = "Alice"},
                new() {Name = "John"}
            };

            _books ??= new List<Book>
            {
                new() {Name = "Kolobok"},
                new() {Name = "Voina I Mir"},
                new() {Name = "Tri Porosenka"}
            };

            _users ??= new List<RegisterDto>
            {
                new() {Email = "admin@gmail.com", Password = "admin", Age = 99},
                new() {Email = "guest@gmail.com", Password = "1111", Age = 99},
                new() {Email = "user@gmail.com", Password = "password", Age = 22},
            };
        }

        public async Task InitializeDbAsync()
        {
            await _db.Database.EnsureCreatedAsync();

            await InitializeAuthorsAsync();

            await InitializeBooksAsync();

            await InitializeUserAsync();

            await InitializeAuthorBooksAsync();
        }

        private async Task InitializeBooksAsync()
        {
            if (!await _db.Books.AnyAsync())
            {
                await _db.Books.AddRangeAsync(_books);

                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeAuthorsAsync()
        {
            if (!await _db.Authors.AnyAsync())
            {
                await _db.Authors.AddRangeAsync(_authors);

                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeAuthorBooksAsync()
        {
            if (!await _db.AuthorBooks.AnyAsync())
            {
                var authorBooks = new List<AuthorBook>();

                var aCount = await _db.Authors.CountAsync();

                var bCount = await _db.Books.CountAsync();

                var count = aCount >= bCount ? aCount : bCount; 

                for (var i = 0; i < count; i++)
                {
                    authorBooks.Add(new AuthorBook() { BookId = _books[i].Id, AuthorId = _authors[i].Id });
                }

                await _db.AuthorBooks.AddRangeAsync(authorBooks);

                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeUserAsync()
        {
            if (!await _db.Users.AnyAsync())
            {
                foreach (var u in _users)
                {
                    await _userManager.CreateAsync(new User
                    {
                        Email = u.Email,
                        Age = u.Age
                    }, u.Password);
                }
            }
        }
    }
}