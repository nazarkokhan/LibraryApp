using System.Collections.Generic;

namespace LibraryApp.DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}