using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace CommandsAndSnippetsAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsApiRepo _commandsRepo;
        private readonly IMapper _mapper;


        // Constructor dependency injection
        public CommandsController(ICommandsApiRepo commandsRepo, IMapper mapper)
        {
            _commandsRepo = commandsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommands()
        {
            try
            {
                var commandItems = await _commandsRepo.GetCommands();
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandByIdAsync(int id)
        {
            try
            {
                var commandItem = await _commandsRepo.GetCommandByIdAsync(id);
                if (commandItem == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            try
            {
                var commandItem = _commandsRepo.GetCommandById(id);
                if (commandItem == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandToCreate)
        {
            try
            {
                var cmdModel = _mapper.Map<Command>(commandToCreate);
                _commandsRepo.CreateCommand(cmdModel);
                await _commandsRepo.SaveCommandsChanges();

                var cmdReadDto = _mapper.Map<CommandReadDto>(cmdModel);

                return CreatedAtRoute(nameof(GetCommandById), new {Id = cmdReadDto.Id}, cmdReadDto);
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

        // We actually hate this
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto commandToUpdate)
        {
            try
            {
                var cmdModelFromRepo = await _commandsRepo.GetCommandByIdAsync(id);

                if (cmdModelFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(commandToUpdate, cmdModelFromRepo);

                //_apiRepo.UpdateCommand(cmdModelFromRepo);

                await _commandsRepo.SaveCommandsChanges();

                // Return updated Resource
                return Ok(_mapper.Map<CommandReadDto>(cmdModelFromRepo));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCommandUpdate(
            int id,
            JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            try
            {
                var cmdModelFromRepo = await _commandsRepo.GetCommandByIdAsync(id);
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

                await _commandsRepo.SaveCommandsChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


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
        public async Task<ActionResult> DeleteCommand(int id)
        {
            try
            {
                var cmdModelFromRepo = await _commandsRepo.GetCommandByIdAsync(id);
                if (cmdModelFromRepo == null)
                {
                    return NotFound();
                }

                _commandsRepo.DeleteCommand(cmdModelFromRepo);
                await _commandsRepo.SaveCommandsChanges();

                // return NoContent();

                // OR return the deleted command to our frontend
                return Ok(_mapper.Map<CommandReadDto>(cmdModelFromRepo));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}