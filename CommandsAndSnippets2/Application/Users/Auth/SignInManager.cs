
using  CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace   CommandsAndSnippets2.Users;

/// <summary>
/// SignInManager
/// </summary>
internal class SignInManager : SignInManager<ApplicationUser>
{
    public SignInManager(
        UserManager<ApplicationUser> userManager, 
        IHttpContextAccessor contextAccessor, 
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
        IOptions<IdentityOptions> optionsAccessor, 
        ILogger<SignInManager<ApplicationUser>> logger, 
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation) : 
        base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        
    }
   


}