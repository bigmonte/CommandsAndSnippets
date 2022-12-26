using MapTo;
using Microsoft.AspNetCore.Identity;

namespace  CommandsAndSnippets2.Application.Users
{
    /// <inheritdoc />
    [MapFrom(typeof(ApplicationRoleUnique))]

    public partial class ApplicationRole : IdentityRole<string>
    {
        /// <inheritdoc />
        public sealed override string Id { get; set; }

        /// <inheritdoc />
        public sealed override string Name { get; set; }

        /// <inheritdoc />
        public sealed override string NormalizedName { get; set; }


    }
}

