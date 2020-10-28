using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public UsersController( IMapper mapper, IUserRepo repo, IAuthManager authManager)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateRawUser(UserCreateDto userToCreate)
        {
            var userModel = _mapper.Map<User>(userToCreate);
            await _repo.CreateUser(userModel);
            await _repo.SaveChanges();

            var readDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtRoute(nameof(GetUserById), new {Id = readDto.Id}, readDto);

        }
        
        
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            var userItem = await _repo.GetUserById(id);
            if (userItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDto>(userItem));
  
        }
        
        
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _repo.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));

        }
     

        
    }
}