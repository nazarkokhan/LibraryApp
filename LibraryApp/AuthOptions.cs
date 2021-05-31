using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp
{
    public class AuthOptions
    {
        public const string Issuer = "LibApiServer";

        public const string Audience = "LibApiClient";

        const string Key = "mysupersecret_secretkey!123";

        public const int Lifetime = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()  => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}