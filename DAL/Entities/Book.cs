using System.Collections.Generic;
using LibraryApp.DAL.Entities.Abstract;

namespace LibraryApp.DAL.Entities
{
    public class Book : EntityBase
    {
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}