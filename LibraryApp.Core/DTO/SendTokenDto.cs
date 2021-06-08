namespace LibraryApp.Core.DTO
{
    public class TokenEmailDto
    {
        public string Token { get; set; }
        
        public string NewEmail { get; set; }
    }

    public class TokenPasswordDto
    {
        public string Token { get; set; }
        
        public string NewPassword { get; set; }

        public string Email { get; set; }
    }
}