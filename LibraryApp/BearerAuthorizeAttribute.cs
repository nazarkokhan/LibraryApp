using System.ComponentModel.DataAnnotations;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        [DataType(DataType.Password)]
        public string tip { get; set; }
        public BearerAuthorizeAttribute(Role roles) : this()
        {
            Roles = roles.ToString();
        }
        
        public BearerAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }

    public class CustomDataTypeAttribute : DataTypeAttribute
    {
        public CustomDataTypeAttribute(DataType dataType) : base(dataType)
        {
        }
    
        public CustomDataTypeAttribute(string customDataType) : base(customDataType)
        {
        }
    }
    
}