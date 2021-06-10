namespace LibraryApp.Core.DTO.Authorization
{
    public class TokenEmailDto
    {
        public TokenEmailDto(string token, string newEmail)
        {
            Token = token;
            NewEmail = newEmail;
        }

        public string Token { get; }

        public string NewEmail { get; }
    }

    public class TokenPasswordDto
    {
        public TokenPasswordDto(string token, string newPassword, string email)
        {
            Token = token;
            NewPassword = newPassword;
            Email = email;
        }

        public string Token { get; }

        public string NewPassword { get; }

        public string Email { get; }
    }
}