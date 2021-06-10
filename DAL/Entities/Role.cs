using LibraryApp.DAL.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public string? RoleDescription { get; set; }
    }
}