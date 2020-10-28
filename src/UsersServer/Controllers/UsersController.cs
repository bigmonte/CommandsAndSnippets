using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersServer.Contracts;
using UsersServer.Dtos;
using UsersServer.Models;


namespace UsersServer.Controllers
{

    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public UsersController( IMapper mapper, IUserRepo repo, IAuthManager authManager)
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
            var loginResultSucceded =  await _authManager.LoginAsync(loginDto.Email, loginDto.Password);
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
        
           
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateRawUser(UserCreateDto userToCreate)
        {
            try
            {
                var userModel = _mapper.Map<User>(userToCreate);
                await _repo.CreateUser(userModel);
                await _repo.SaveChanges();

                var readDto = _mapper.Map<UserReadDto>(userModel);

                return CreatedAtRoute(nameof(GetUserById), new {Id = readDto.Id}, readDto);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            try
            {
                var userItem = await _repo.GetUserById(id);
                if (userItem == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UserReadDto>(userItem));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
  
        }
        
        // TODO Separate and rename to AuthController
        
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            try
            {
                var users = await _repo.GetUsers();
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
     

        
    }
}