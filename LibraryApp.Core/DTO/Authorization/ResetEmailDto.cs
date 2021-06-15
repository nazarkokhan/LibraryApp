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

    public class ResetEmailDto
    {
        public ResetEmailDto(string newEmail)
        {
            NewEmail = newEmail;
        }

        public string NewEmail { get; }
    }
}