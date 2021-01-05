using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersServer.Contracts;
using UsersServer.Dtos;
using UsersServer.Models;

namespace UsersServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]     
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUserManager _userManager;

        public AuthController( IMapper mapper, IAuthManager authManager, IUserManager userManager)
        {
            _mapper = mapper;
            _authManager = authManager;
            _userManager = userManager;
        }

        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> SignupUserAsync(UserSignupDto userSignupDto)
        {
            return await _authManager.CreateUserAsync(userSignupDto);
        }

        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<IdentityResult>> GetTokenAsync(UserLoginDto loginDto)
        {
            var loginResultSucceded =  await _authManager.GetToken(loginDto);

            if (loginResultSucceded != null)
            {
                return Ok(_mapper.Map<AccessToken>(loginResultSucceded));

            }
            return Problem();
        }
        
            
        [AllowAnonymous]
        [HttpPost("login2")]
        public async Task<ActionResult<IdentityResult>> DoLoginAsync(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);


            if (user != null)
            {
                if(await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return Json(true);
                }
            }

            return Json(false);
        }

        [HttpGet("test")]
        public ActionResult TestRequest()
        {
            return Ok(new {Message = "Test"});
        }
      
        
    }
}