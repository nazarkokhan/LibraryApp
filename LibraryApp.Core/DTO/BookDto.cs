using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LibraryApp.Core.DTO
{
    public class BookDto
    {
        public BookDto(int id, string name, IEnumerable<GetAuthorDto> authors)
        {
            Id = id;
            Name = name;
            Authors = authors;
        }
        public int Id { get; }

        public string Name { get; }

        [DataMember]
        public IEnumerable<GetAuthorDto> Authors { get; }
    }

    public class CreateBookDto
    {
        public CreateBookDto(string name, IEnumerable<int> authorIds)
        {
            Name = name;
            AuthorIds = authorIds;
        }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; }

        [Required] public IEnumerable<int> AuthorIds { get; }
    }

    public class UpdateBookDto
    {
        public UpdateBookDto(int id, string name, IEnumerable<int> authorIds)
        {
            Id = id;
            Name = name;
            AuthorIds = authorIds;
        }

        [Required] [Range(0, int.MaxValue)] public int Id { get; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; }

        [Required] public IEnumerable<int> AuthorIds { get; }
    }
}