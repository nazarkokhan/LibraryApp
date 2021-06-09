using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.BLL
{
    public class AuthOptions
    {
        public const string Issuer = "LibApiServer";

        public const string Audience = "LibApiClient";

        private const string Key = "mysupersecret_secretkey!123";

        public const int Lifetime = 60;

        public static SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.ASCII.GetBytes(Key));
    }
}