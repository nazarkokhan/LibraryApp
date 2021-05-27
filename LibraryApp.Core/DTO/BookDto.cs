using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DAL.DTO
{
    public class GetBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GetAuthorDto> Authors { get; set; }
    }

    public class CreateBookDto
    {
        [Required]
        [MinLength(1)] [MaxLength(1000)]
        public string Name { get; set; }

        [Required]
        public IEnumerable<int> AuthorIds { get; set; }
    }

    public class UpdateBookDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)][MaxLength(1000)]
        public string Name { get; set; }

        [Required]
        public IEnumerable<int> AuthorIds { get; set; }
    }
}
