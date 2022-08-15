using LibraryApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.EF;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class LibContext : IdentityDbContext<User, Role, int>
{
    public LibContext(DbContextOptions<LibContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<AuthorBook> AuthorBooks { get; set; }

    public override DbSet<User> Users { get; set; }
}