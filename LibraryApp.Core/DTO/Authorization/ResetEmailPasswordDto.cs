namespace LibraryApp.Core.DTO.Authorization
{
    public class ResetEmailDto
    {
        public ResetEmailDto(string newEmail)
        {
            NewEmail = newEmail;
        }

        public string NewEmail { get; }
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