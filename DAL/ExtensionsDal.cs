using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL
{
    public static class ExtensionDto
    {
        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> func)
        {
            foreach (var e in enumerable)
            {
                await func(e);
            }
        }
    }
}