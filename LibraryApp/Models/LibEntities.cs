using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }

    public class AuthorBook
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Author Author { get; set; }

        public int AuthorId { get; set; }
        public Book Book { get; set; }
    }
}
