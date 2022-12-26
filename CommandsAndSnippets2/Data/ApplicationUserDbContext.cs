using BlueWest.EfMethods;
using CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippets2.Domain
{
    /// <summary>
    /// Application User Db Context
    /// </summary>
    [EfGenerator]
    public class ApplicationUserDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>
    {
        /// <inheritdoc />
        [EfGetMany(typeof(ApplicationUserClaimUnique))]
        public sealed override DbSet<ApplicationUserClaim> UserClaims { get; set; }

        /// <inheritdoc />
        [EfGetMany(typeof(ApplicationUserRoleUnique))]
        public sealed override DbSet<ApplicationUserRole> UserRoles { get; set; }

        /// <inheritdoc />
        [EfGetMany(typeof(ApplicationRoleUnique))]
        public sealed override DbSet<ApplicationRole> Roles { get; set; }

        /// <inheritdoc />
        [EfGetMany(typeof(ApplicationRoleClaimUnique))]
        public sealed override DbSet<ApplicationRoleClaim> RoleClaims { get; set; }

        [EfGetMany(typeof(ApplicationUserUnique))]
        [EfUpdateMethods( updateType: typeof(ApplicationUserUnique), returnType: typeof(ApplicationUserUnique), keyPropertyMemberName: nameof(ApplicationUserUnique.Id))]
        public sealed override DbSet<ApplicationUser> Users { get; set; }

        /// <inheritdoc />
        #region Initialization
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
        }


        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCurrentDbModel();

        }

        #endregion

    }
}