using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO.Authorization
{
    public class LogInUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}