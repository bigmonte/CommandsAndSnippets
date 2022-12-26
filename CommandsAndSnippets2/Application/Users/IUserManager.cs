
using System.Security.Claims;
using  CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Identity;

namespace   CommandsAndSnippets2.Users
{
    public interface IUserManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        
        /// <summary>
        /// Checks for user password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        
        /// <summary>
        /// Find by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ApplicationUser> FindByEmailAsync(string email);

        string GetUserId(ClaimsPrincipal principal);

    }
}

