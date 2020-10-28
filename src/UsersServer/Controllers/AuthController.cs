using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersServer.Contracts;
using UsersServer.Dtos;

namespace UsersServer.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController( IMapper mapper, IAuthManager authManager)
        {
            _mapper = mapper;
            _authManager = authManager;
        }

        
        [AllowAnonymous]
        [HttpPost("/api/register")]
        public async Task<ActionResult<IdentityResult>> SignupUserAsync(UserSignupDto userSignupDto)
        {
            return await _authManager.CreateUserAsync(userSignupDto);
        }

        
        [AllowAnonymous]
        [HttpPost("api/login")]
        public async Task<ActionResult<IdentityResult>> GetTokenAsync(UserLoginDto loginDto)
        {
            var loginResultSucceded =  await _authManager.GetToken(loginDto);

            if (loginResultSucceded != null)
            {
                return Ok(_mapper.Map<AccessToken>(loginResultSucceded));

            }
            return Problem();
        }
    }
}