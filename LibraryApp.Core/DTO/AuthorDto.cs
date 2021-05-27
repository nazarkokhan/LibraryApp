using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DAL.DTO
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
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)] [MaxLength(1000)]
        public string Name { get; set; }
    }
}
