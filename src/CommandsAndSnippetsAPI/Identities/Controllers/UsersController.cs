using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos.User;
using CommandsAndSnippetsAPI.Identities.Contracts;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// TODO Move to different project

namespace CommandsAndSnippetsAPI.Identities.Controllers
{
    // TODO: Only Admin role users would be able to access this 

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

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> SignupUserAsync(UserSignupDto userSignupDto)
        {
            return await _authManager.CreateUserAsync(userSignupDto);
        }
        
           
        //[HttpPost]
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