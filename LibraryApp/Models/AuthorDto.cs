using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class GetAuthorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateAuthorDto
    {
        public string Name { get; set; }
    }

    public class PutAuthorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
