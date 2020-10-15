using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public sealed class IdentitiesContext : IdentityDbContext<User>
    {
        public IdentitiesContext(DbContextOptions<IdentitiesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public override DbSet<User> Users { get; set; }

        //public override DbSet<TUserRole> UserRoles { get; set; }
          
        //public override DbSet<TRole> Roles { get; set; }

        //public override DbSet<TRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}