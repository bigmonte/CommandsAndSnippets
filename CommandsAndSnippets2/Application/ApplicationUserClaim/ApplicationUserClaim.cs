using Microsoft.AspNetCore.Identity;
using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    [MapFrom(typeof(ApplicationUserClaimUnique))]

    /// <inheritdoc />
    public partial class ApplicationUserClaim : IdentityUserClaim<string>
    {
        /// <inheritdoc />
        public sealed override int Id { get; set; }

        #region User
        /// <inheritdoc />
        public sealed override string UserId { get; set; }
    
        /// <summary>
        /// Application User entity
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }
    
        #endregion
    
        /// <inheritdoc />
        public sealed override string ClaimType { get; set; }

        /// <inheritdoc />
        public sealed override string ClaimValue { get; set; }

    }
}
