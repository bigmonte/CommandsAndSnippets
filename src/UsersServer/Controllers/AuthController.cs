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
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController( IMapper mapper, IUserRepo repo, IAuthManager authManager)
        {
            _mapper = mapper;
            _repo = repo;
            _authManager = authManager;
        }

        
        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<ActionResult<IdentityResult>> SignupUserAsync(UserSignupDto userSignupDto)
        {
            return await _authManager.CreateUserAsync(userSignupDto);
        }
        [AllowAnonymous]
        
        [HttpPost("/login")]
        public async Task<ActionResult<IdentityResult>> LoginUserAsync(UserLoginDto loginDto)
        {
            var loginResultSucceded =  await _authManager.VerifyLoginAsync(loginDto.Email, loginDto.Password);
            if (loginResultSucceded) return Accepted();
            return Problem();
        }
        
        [AllowAnonymous]
        [HttpPost("/token")]
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