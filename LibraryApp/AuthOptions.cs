using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp
{
    public class AuthOptions
    {
        public const string Issuer = "LibApiServer";

        public const string Audience = "LibApiClient";

        const string Key = "mysupersecret_secretkey!123";

        public const int Lifetime = 60;

        public static SymmetricSecurityKey SymmetricSecurityKey => new (Encoding.ASCII.GetBytes(Key));
    }
}