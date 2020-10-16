using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Data.Identities;
using CommandsAndSnippetsAPI.Dtos;
using CommandsAndSnippetsAPI.Dtos.User;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommandsAndSnippetsAPI.Controllers
{
    // TODO: Only Admin role users would be able to access this 

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly ILoginManager _loginManager;

        public UsersController( IMapper mapper, IUserRepo repo, ILoginManager loginManager)
        {
            _mapper = mapper;
            _repo = repo;
            _loginManager = loginManager;
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

        // TODO: Move most of this functionality to another layer to interact with Hasher.cs
        
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto userToCreate)
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
    
            /*  CreatedAtRoute method will:
             *      - Return 201: Created 201 status code
             *      - Pass back the created resource in body response
             *      - Pass back the URI (or route) in the response header
             */
            
            // Further references:
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute
            
        }

        
    }
}