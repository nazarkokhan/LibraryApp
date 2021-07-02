using System.ComponentModel.DataAnnotations;
using LibraryApp.DAL.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public int Age { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
    }
}