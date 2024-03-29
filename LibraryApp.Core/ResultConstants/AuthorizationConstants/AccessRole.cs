﻿using System;

namespace LibraryApp.Core.ResultConstants.AuthorizationConstants;

[Flags]
public enum AccessRole
{
    /// <summary>
    /// Represents User role.
    /// </summary>
    User = 1,
        
    /// <summary>
    /// Represents Admin role.
    /// </summary>
    Admin = 2
}