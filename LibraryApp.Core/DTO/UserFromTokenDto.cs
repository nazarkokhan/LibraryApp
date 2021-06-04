using System.Security.Claims;

namespace LibraryApp.Core.DTO
{
    public class UserFromTokenDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}