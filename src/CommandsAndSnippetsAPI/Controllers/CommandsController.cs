using System.Collections.Generic;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace CommandsAndSnippetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsAndSnippetsAPIRepo _commandsRepo;
        private readonly IMapper _mapper;
        
        
        // Constructor dependency injection
        public CommandsController(ICommandsAndSnippetsAPIRepo commandsRepo, IMapper mapper)
        {
            _commandsRepo = commandsRepo;
            _mapper = mapper;
        }
        
        [HttpGet]   
        public ActionResult<IEnumerable<CommandReadDto>> GetCommands()
        {
            var commandItems = _commandsRepo.GetCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _commandsRepo.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandToCreate)
        {
            var cmdModel = _mapper.Map<Command>(commandToCreate);
            _commandsRepo.CreateCommand(cmdModel);
            _commandsRepo.SaveCommandsChanges();

            var cmdReadDto = _mapper.Map<CommandReadDto>(cmdModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = cmdReadDto.Id}, cmdReadDto);

            /*  ^ this method will:
             *      - Return 201: Created 201 status code
             *      - Pass back the created resource in body response
             *      - Pass back the URI (or route) in the response header
             */
            
            // Further references:
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute
            
        }
        
        // We actually hate this
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandToUpdate)
        {
            var cmdModelFromRepo = _commandsRepo.GetCommandById(id);
            
            if (cmdModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandToUpdate, cmdModelFromRepo);
            
            //_apiRepo.UpdateCommand(cmdModelFromRepo);

            _commandsRepo.SaveCommandsChanges();

            // Return 204 (No Content)
            //return NoContent();
            var cmd = new CommandReadDto{
                Id = id,
                HowTo = commandToUpdate.HowTo,
                CommandLine = commandToUpdate.CommandLine,
                Platform = commandToUpdate.Platform
            }; 
            
            // create a new ReadDTO Because we want the ID?
            // TODO fix that 

            // Return updated Resource
            return Ok(_mapper.Map<CommandReadDto>(cmd));

        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(
            int id, 
            JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var cmdModelFromRepo = _commandsRepo.GetCommandById(id);
            if (cmdModelFromRepo == null)
            {
                return NotFound();
            }

            var cmdToPatch = _mapper.Map<CommandUpdateDto>(cmdModelFromRepo);
            patchDoc.ApplyTo(cmdToPatch, ModelState);

            if (!TryValidateModel(cmdToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(cmdToPatch, cmdModelFromRepo);

            _commandsRepo.UpdateCommand(cmdModelFromRepo);

            _commandsRepo.SaveCommandsChanges();

            return NoContent();
            
            /*            Example patch operation
             * [
                    {
                        "op" : "replace",
                        "path" : "/howto",
                        "value": "List all EF migrations"
                    }
                ]
             */
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var cmdModelFromRepo = _commandsRepo.GetCommandById(id);
            if (cmdModelFromRepo == null)
            {
                return NotFound();
            }
            _commandsRepo.DeleteCommand(cmdModelFromRepo);
            _commandsRepo.SaveCommandsChanges();

            // return NoContent();

            // OR return the deleted command to our frontend
            return Ok(_mapper.Map<CommandReadDto>(cmdModelFromRepo));
        }
    }
}