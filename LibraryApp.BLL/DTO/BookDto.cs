using System.Collections.Generic;

namespace LibraryApp.BLL.DTO
{
    public class GetBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GetAuthorDto> Authors { get; set; }
    }

    public class CreateBookDto
    {
        public string Name { get; set; }

        public IEnumerable<int> AuthorIds { get; set; }
    }

    public class PutBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> AuthorIds { get; set; }
    }
}
