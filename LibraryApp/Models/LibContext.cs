using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models
{
    public class LibContext : DbContext
    {
        public LibContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
}
