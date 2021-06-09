namespace LibraryApp.Core.DTO.Authorization
{
    public class EditUserDto
    {
        public string NewEmail { get; set; }

        public int NewAge { get; set; }

        public string NewPassword { get; set; }

        public string CurrentEmail { get; set; }

        public string CurrentPassword { get; set; }
    }
}