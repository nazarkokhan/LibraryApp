using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp
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