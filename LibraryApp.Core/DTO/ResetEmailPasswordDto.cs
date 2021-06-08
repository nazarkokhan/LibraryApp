namespace LibraryApp.Core.DTO
{
    public class ResetEmailDto
    {
        public string NewEmail { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
    }
}