namespace LibraryApp.Core.DTO.Authorization
{
    public class EditUserDto
    {
        public EditUserDto(string newEmail, int newAge, string newPassword, string id)
        {
            NewEmail = newEmail;
            NewAge = newAge;
            NewPassword = newPassword;
            Id = id;
        }

        public string NewEmail { get; }

        public int NewAge { get; }

        public string NewPassword { get; }

        public string Id { get; }
    }
}