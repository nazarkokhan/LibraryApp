using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp
{
    public static class ExtensionDal
    {
        public static void ConfigurePassword(this IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> func)
        {
            foreach (var e in enumerable)
            {
                await func(e);
            }
        }
    }
}