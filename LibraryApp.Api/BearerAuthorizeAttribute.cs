namespace LibraryApp.Api;

using Core.ResultConstants.AuthorizationConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

public class BearerAuthorizeAttribute : AuthorizeAttribute
{
    public BearerAuthorizeAttribute(AccessRole accessRoles) : this()
    {
        Roles = accessRoles.ToString();
    }

    public BearerAuthorizeAttribute()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}