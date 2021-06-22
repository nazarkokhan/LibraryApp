using System.ComponentModel.DataAnnotations;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;

namespace LibraryApp.Core.DTO.Authorization
{
    public class RegisterDto
    {
        public RegisterDto(string email, string password, int age)
        {
            Email = email;
            Password = password;
            Age = age;
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; }

        [Required]
        [Password]
        public string Password { get; }

        [Required] 
        [Range(0, 150)] 
        public int Age { get; }
    }

    public class MyDataTypeAttribute : DataTypeAttribute
    {
        public MyDataTypeAttribute(DataType dataType) : base(dataType)
        {
        }

        public MyDataTypeAttribute(string customDataType) : base(customDataType)
        {
        }

        public override bool IsValid(object? value)
        {
            if (CustomDataType is "MyPassword" && value is string strValue)
            {
                return strValue.Length >= IdentityPasswordConstants.RequiredLength;
            }

            return false;
        }
    }

    public class Password : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string strValue)
            {
                return strValue.Length >= IdentityPasswordConstants.RequiredLength;
            }

            return false;
        }
    }
}