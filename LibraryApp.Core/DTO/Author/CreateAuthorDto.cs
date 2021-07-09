using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO.Author
{
    public class CreateAuthorDto
    {
        public CreateAuthorDto(string name)
        {
            Name = name;
        }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; }
    }
}