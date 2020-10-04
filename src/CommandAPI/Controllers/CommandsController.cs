using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CommandAPI.Dtos;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _apiRepo;
        private readonly IMapper _mapper;
        
        
        // Constructor dependency injection
        public CommandsController(ICommandAPIRepo apiRepo, IMapper mapper)
        {
            _apiRepo = apiRepo;
            _mapper = mapper;
        }
        [HttpGet]   
        public ActionResult<IEnumerable<CommandReadDto>> GetCommands()
        {
            var commandItems = _apiRepo.GetCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _apiRepo.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }
    }
}