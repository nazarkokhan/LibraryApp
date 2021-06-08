using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.BLL
{
    public class AuthOptions
    {
        public AuthOptions()
        {
            var a = new AuthOptions();
        }

        public const string Issuer = "LibApiServer";

        public const string Audience = "LibApiClient";

        public const string Key = "mysupersecret_secretkey!123";

        public const int Lifetime = 60;

        public static SymmetricSecurityKey SymmetricSecurityKey => new (Encoding.ASCII.GetBytes(Key));
    }
}