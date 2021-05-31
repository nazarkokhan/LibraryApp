namespace LibraryApp.Core.DTO
{
    public class GetUserDto
    {
        public int  Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool Admin { get; set; }
    }

    public class CreateUserDto
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class LogInUserDto
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}