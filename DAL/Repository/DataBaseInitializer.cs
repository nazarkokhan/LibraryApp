using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.Extensions;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Repository;

using Core.ResultConstants.AuthorizationConstants;
using Microsoft.EntityFrameworkCore;

public class DataBaseInitializer
{
    private readonly LibContext _db;

    private readonly RoleManager<Role> _roleManager;

    private readonly UserManager<User> _userManager;

    private readonly List<Author> _authors;

    private readonly List<Book> _books;

    private readonly List<Role> _roles;

    private readonly List<RegisterDto> _users;


    public DataBaseInitializer(
        LibContext db,
        RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _db = db;
        _roleManager = roleManager;
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

        _roles ??= new List<Role>
        {
            new() {Name = AccessRole.Admin.ToString(), RoleDescription = "Has a admin access"},
            new() {Name = AccessRole.User.ToString(), RoleDescription = "Role for all registered users"}
        };

        _users ??= new List<RegisterDto>
        {
            new("admin@gmail.com", "adminAccess", 99),
            new("guest@gmail.com", "guestAccess", 99),
            new("user@gmail.com", "userAccess", 99)
        };
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
                authorBooks.Add(new AuthorBook {BookId = _books[i].Id, AuthorId = _authors[i].Id});

            await _db.AuthorBooks.AddRangeAsync(authorBooks);

            await _db.SaveChangesAsync();
        }
    }

    private async Task InitializeRolesAsync()
    {
        if (!await _roleManager.Roles.AnyAsync())
            await _roles.ForEachAsync(async r => await _roleManager.CreateAsync(r));
    }

    private async Task InitializeUsersAsync()
    {
        if (!await _userManager.Users.AnyAsync())
            await _users.ForEachAsync(
                async u =>
                {
                    var user = new User
                    {
                        Email = u.Email,
                        UserName = u.Email,
                        Age = u.Age
                    };

                    await _userManager.CreateAsync(user, u.Password);

                    user.EmailConfirmed = true;

                    if (user.Id != 1)
                        await _userManager.AddToRoleAsync(user, AccessRole.User.ToString());

                    else
                        await _userManager.AddToRoleAsync(user, AccessRole.Admin.ToString());
                });
    }
}