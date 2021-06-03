namespace LibraryApp.Core.DTO
{
    public class ResetEmailDto
    {
        public string OldEmail { get; set; }

        public string NewEmail { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }
    }
}