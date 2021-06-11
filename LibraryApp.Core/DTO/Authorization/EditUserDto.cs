namespace LibraryApp.Core.DTO.Authorization
{
    public class EditUserDto
    {
        public EditUserDto(string newEmail, int newAge, string newPassword, string currentEmail)
        {
            NewEmail = newEmail;
            NewAge = newAge;
            NewPassword = newPassword;
            CurrentEmail = currentEmail;
        }

        public string NewEmail { get; }

        public int NewAge { get; }

        public string NewPassword { get; }

        public string CurrentEmail { get; }
    }
}