using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO.Authorization
{
    public class TokenPasswordDto
    {
        public TokenPasswordDto(string newPassword, string email, string confirmNewPassword, string token)
        {
            Email = email;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
            Token = token;
        }

        [DataType(DataType.EmailAddress)] 
        public string Email { get; }

        [DataType(DataType.Password)] 
        public string NewPassword { get; }

        [Compare("NewPassword", ErrorMessage = "Passwords have to be equal")]
        public string ConfirmNewPassword { get; }

        public string Token { get; }
    }

    public class ResetPasswordDto
    {
        public ResetPasswordDto(string email)
        {
            Email = email;
        }

        [DataType(DataType.EmailAddress)] public string Email { get; }
    }
}