using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class Role : IdentityRole<int>
    {
        public string RoleDescription { get; set; }
    }
}