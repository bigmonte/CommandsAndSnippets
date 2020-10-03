using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _apiRepo;
        
        // Constructor dependency injection
        public CommandsController(ICommandAPIRepo apiRepo)
        {
            _apiRepo = apiRepo;
        }
        [HttpGet]   
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            var commandItems = _apiRepo.GetCommands();
            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _apiRepo.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            return Ok(commandItem);
        }
    }
}