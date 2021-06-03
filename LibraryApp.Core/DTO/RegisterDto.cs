using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(0, 150)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}