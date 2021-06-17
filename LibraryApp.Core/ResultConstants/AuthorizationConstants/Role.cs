using System;

namespace LibraryApp.Core.ResultConstants.AuthorizationConstants
{
    [Flags]
    public enum Role
    {
        /// <summary>
        /// Represents Admin role.
        /// </summary>
        User = 1,
        
        /// <summary>
        /// Represents User role.
        /// </summary>
        Admin = 2
    }
}