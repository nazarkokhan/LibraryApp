﻿using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public int Age { get; set; }
    }
}