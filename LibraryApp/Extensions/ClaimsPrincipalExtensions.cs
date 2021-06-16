using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (int.TryParse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
                return id;

            return -1;
        }
        
    }
}