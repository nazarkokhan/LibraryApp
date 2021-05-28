using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class GetAuthorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateAuthorDto
    {
        [Required]
        [MinLength(1)] [MaxLength(1000)]
        public string Name { get; set; }
    }

    public class UpdateAuthorDto
    {
        public UpdateAuthorDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; }

        [Required]
        [MinLength(1)] [MaxLength(1000)]
        public string Name { get; }
    }
}
