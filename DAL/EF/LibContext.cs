using LibraryApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.EF
{
    public class LibContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LibContext(DbContextOptions options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Author> Authors { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Book> Books { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<AuthorBook> AuthorBooks { get; set; }
    }

    //public class LibDbInitializer : DropCreateDatabaseAlways<LibContext>
    //{
    //    protected override void Seed(LibContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}