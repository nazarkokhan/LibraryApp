using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public int NewAge { get; set; }
    }
}