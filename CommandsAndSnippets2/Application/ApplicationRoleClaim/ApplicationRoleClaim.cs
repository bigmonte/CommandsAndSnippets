using System;
using Microsoft.AspNetCore.Identity;
using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    
    /// <inheritdoc />
    [MapFrom(typeof(ApplicationRoleClaimUnique))]

    public partial class ApplicationRoleClaim : IdentityRoleClaim<string>
    {

        public sealed override int Id { get; set; }

        #region ApplicationRole
        public sealed override string RoleId { get; set; }
        
        public ApplicationRole ApplicationRole { get; set; }

        #endregion

        public sealed override string ClaimType { get; set; }

        public sealed override string ClaimValue { get; set; }

    }
}
