using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL
{
    public static class Roles
    {
        public const string Admin = "admin";

        public const string User = "user";
    }

    public static class Extensions
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
            foreach (var e in enumerable) await func(e);
        }

        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, int page, int items)
        {
            return queryable
                .Skip((page - 1) * items)
                .Take(items);
        }
        
        // public static IQueryable<T> Search<T>(this IQueryable<T> queryable, string search)
        // {
        //     return queryable.Where(q => q.)
        // }
    }
}