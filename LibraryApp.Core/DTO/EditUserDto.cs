namespace LibraryApp.Core.DTO
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