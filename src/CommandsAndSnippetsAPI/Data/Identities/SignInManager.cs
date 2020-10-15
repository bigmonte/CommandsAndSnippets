using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public class SignInManager : SignInManager<User>
    {
        private readonly IHasher _hasher;

        public SignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation, IHasher hasher) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes, confirmation)
        {
            _hasher = hasher;
        }
        
    }
}