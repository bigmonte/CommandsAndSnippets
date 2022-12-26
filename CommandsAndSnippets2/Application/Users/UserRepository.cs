using CommandsAndSnippets2.Application.Users;
using CommandsAndSnippets2.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippets2.Application
{
    /// <summary>
    /// Users Repository
    /// </summary>
    public class UserRepository : UserStore<ApplicationUser,
        ApplicationRole, 
        ApplicationUserDbContext, 
        string,
        ApplicationUserClaim, 
        ApplicationUserRole, 
        ApplicationUserLogin, 
        ApplicationUserToken,
        ApplicationRoleClaim>
    {
        private readonly ApplicationUserDbContext _context;

        /// <summary>
        /// User repository 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="describer"></param>
        public UserRepository(ApplicationUserDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
            _context = context;
        }

        /// <summary>
        /// Get Application Users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        /// <inheritdoc />
        public override Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.PasswordHash);
        }
        
        /// <inheritdoc />
        public override Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
  
    }
}
