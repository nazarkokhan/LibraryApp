using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
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
            foreach (var e in enumerable)
            {
                await func(e);
            }
        }
    }

    //public class Roles
    //{
    //    private Roles(string value) => Value = value;

    //    public string Value { get; init; }

    //    public static Roles Admin => new("admin");

    //    public static Roles User => new("user");
    //}
}