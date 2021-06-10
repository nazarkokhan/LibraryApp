using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Extensions
{
    public static class Extensions
    {
        public static void ConfigurePassword(this IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        }
    }
}