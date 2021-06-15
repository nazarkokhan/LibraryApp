namespace LibraryApp.Core.DTO.Authorization
{
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

    public class ResetPasswordDto
    {
        public ResetPasswordDto(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}