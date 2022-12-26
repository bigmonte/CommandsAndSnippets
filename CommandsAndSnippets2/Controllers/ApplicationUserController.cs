using System.Security.Claims;
using CommandsAndSnippets2.Auth;
using CommandsAndSnippets2.Domain;
using CommandsAndSnippets2.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommandsAndSnippets2.Controllers
{
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IAuthManager _authManager;
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, IAuthManager authManager, ApplicationUserDbContext applicationUserDbContext)
        {
            _logger = logger;
            _authManager = authManager;
            _applicationUserDbContext = applicationUserDbContext;
        }

        [HttpGet("users")]
        public ActionResult GetUsers()
        {
            var (success, result) = _applicationUserDbContext.GetUsers();
            if (success)
            {
                return Ok(result);
            }

            return Ok();
        }
        
        /// <summary>
        /// Check for the presence of a valid cookie
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("auth/status")]
        public  ActionResult GetAuthStatus()
        {
            if (HttpContext.User.Identity is {IsAuthenticated: true})
            {
                return Ok(new {IsAuthenticated = true});
            }

            return Ok(new {IsAuthenticated = false});
        }
        [AllowAnonymous]
        [HttpPost("auth/signin")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var (identity,success) =
                await _authManager.DoLogin(loginRequest);
            
            
            if (!success) return Ok(new { success = false, redirectUrl = "/login" });

            if (success)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.Add(SessionConstants.DefaultSessionMaxAge)
                    });
                
                return Ok(new { success = true, redirectUrl = "/" });
            }

            return Ok(new { success = false, redirectUrl = "/login" });
            
        }
        
        [AllowAnonymous]
        [HttpPost("auth/signup")]
        public async Task<IActionResult> Signup(SignUpRequest signUpRequest)
        {
            var result = await _authManager.CreateUserAsync(signUpRequest);

            if (result.Succeeded)
            {
                return Ok("Success");
            }

            return new ForbidResult(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
