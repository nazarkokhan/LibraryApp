namespace LibraryApp.Core.DTO.Authorization
{
    public class UserFromTokenDto
    {
        public int Id { get; init; }

        public string Email { get; init; }

        public string Role { get; init; }
    }
}