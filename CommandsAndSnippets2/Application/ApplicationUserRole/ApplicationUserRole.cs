using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    
    /// <inheritdoc />
    [MapFrom(typeof(ApplicationUserRoleUnique))]

    public partial class ApplicationUserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// User entity of this role
        /// </summary>
        public ApplicationUser User { get; set; }
    
        public ApplicationRole ApplicationRole { get; set; }

        /// <inheritdoc />
        public sealed override string UserId { get; set; }

        /// <inheritdoc />
        public sealed override string RoleId { get; set; }
        
    }
}
