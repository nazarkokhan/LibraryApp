using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
