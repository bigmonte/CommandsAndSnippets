using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _apiRepo.GetCommandById(id);
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
            _apiRepo.CreateCommand(cmdModel);
            _apiRepo.SaveChanges();

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
            var cmdModelFromRepo = _apiRepo.GetCommandById(id);
            
            if (cmdModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandToUpdate, cmdModelFromRepo);
            
            //_apiRepo.UpdateCommand(cmdModelFromRepo);

            _apiRepo.SaveChanges();

            // Return 204 (No Content)
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(
            int id, 
            JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var cmdModelFromRepo = _apiRepo.GetCommandById(id);
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

            _apiRepo.UpdateCommand(cmdModelFromRepo);

            _apiRepo.SaveChanges();

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
            var cmdModelFromRepo = _apiRepo.GetCommandById(id);
            if (cmdModelFromRepo == null)
            {
                return NotFound();
            }
            _apiRepo.DeleteCommand(cmdModelFromRepo);
            _apiRepo.SaveChanges();

            return NoContent();
        }
    }
}