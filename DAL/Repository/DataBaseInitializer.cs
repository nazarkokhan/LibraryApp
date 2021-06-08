using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private readonly RoleManager<Role> _roleManager;

        private readonly List<Book> _books;

        private readonly List<Author> _authors;

        private readonly List<RegisterDto> _users;

        private readonly List<Role> _roles;

        public DataBaseInitializer(LibContext db, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _db = db;

            _userManager = userManager;

            _roleManager = roleManager;

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

            _roles ??= new List<Role>
            {
                new() {Name = Roles.Admin, RoleDescription = "Has a admin access"},
                new() {Name = Roles.User, RoleDescription = "Role for all registered users"}
            };

            // _users ??= new List<RegisterDto>
            // {
            //     new() {Email = "admin@gmail.com", Password = "adminAccess", Age = 99},
            //     new() {Email = "guest@gmail.com", Password = "guestAccess", Age = 99},
            //     new() {Email = "user@gmail.com", Password = "userAccess", Age = 99},
            // };
        }

        public async Task InitializeDbAsync()
        {
            await _db.Database.EnsureCreatedAsync();

            await InitializeAuthorsAsync();

            await InitializeBooksAsync();

            await InitializeAuthorBooksAsync();

            await InitializeRolesAsync();

            await InitializeUsersAsync();
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
                    authorBooks.Add(new AuthorBook { BookId = _books[i].Id, AuthorId = _authors[i].Id });
                }

                await _db.AuthorBooks.AddRangeAsync(authorBooks);

                await _db.SaveChangesAsync();
            }
        }

        private async Task InitializeRolesAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roles.ForEachAsync(async r => await _roleManager.CreateAsync(r));
            }
        }

        private async Task InitializeUsersAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                await _users.ForEachAsync(async u =>
                {
                    var user = new User
                    {
                        Email = u.Email,
                        UserName = u.Email,
                        Age = u.Age
                    };

                    await _userManager.CreateAsync(user, u.Password);

                    if (user.Id != 1)
                        await _userManager.AddToRoleAsync(user, Roles.User);

                    else
                        await _userManager.AddToRoleAsync(user, Roles.Admin);
                });
            }
        }
    }
}