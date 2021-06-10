namespace LibraryApp.Core.DTO.Authorization
{
    public class UserFromTokenDto
    {
        public UserFromTokenDto(int id, string email, string role)
        {
            Id = id;
            Email = email;
            Role = role;
        }

        public int Id { get; }

        public string Email { get; }

        public string Role { get; }
    }
}