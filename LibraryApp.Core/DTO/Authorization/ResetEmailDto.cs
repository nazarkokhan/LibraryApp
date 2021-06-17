using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO.Authorization
{
    public class ResetEmailDto
    {
        public ResetEmailDto(string newEmail, string confirmNewEmail)
        {
            NewEmail = newEmail;
            ConfirmNewEmail = confirmNewEmail;
        }

        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; }

        [Compare("NewEmail", ErrorMessage = "Emails have to be equal")]
        public string ConfirmNewEmail { get; }
    }
}