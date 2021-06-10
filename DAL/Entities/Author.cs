using System.Collections.Generic;
using System.Runtime.Serialization;
using LibraryApp.DAL.Entities.Abstract;

namespace LibraryApp.DAL.Entities
{
    public class Author : EntityBase
    {
        public string Name { get; set; }

        [DataMember] 
        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}