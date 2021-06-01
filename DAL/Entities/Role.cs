using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class Role : IdentityRole<int>
    {
        public int TestRoleData { get; set; }
    }
}