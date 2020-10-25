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
    
    public class SnippetsController : ControllerBase
    {
        private readonly ISnippetsAPIRepo _snippetsRepo;
        private readonly IMapper _mapper;
        
        
        // Constructor dependency injection
        public SnippetsController(ISnippetsAPIRepo snippetsRepo, IMapper mapper)
        {
            _snippetsRepo = snippetsRepo;
            _mapper = mapper;
        }
        
        [HttpGet]   
        public ActionResult<IEnumerable<SnippetReadDto>> GetSnippets()
        {
            var snippetItems = _snippetsRepo.GetSnippets();
            return Ok(_mapper.Map<IEnumerable<SnippetReadDto>>(snippetItems));
        }
        
        [HttpGet("{id}", Name = "GetSnippetById")]
        public ActionResult<SnippetReadDto> GetSnippetById(int id)
        {
            var snippetItem = _snippetsRepo.GetSnippetById(id);
            if (snippetItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SnippetReadDto>(snippetItem));
        }

        [HttpPost]
        public ActionResult<SnippetReadDto> CreateSnippet(SnippetCreateDto snippetToCreate)
        {
            var snippet = _mapper.Map<Snippet>(snippetToCreate);
            
            _snippetsRepo.CreateSnippet(snippet);
            _snippetsRepo.SaveSnippetsChanges();

            var snippetReadDto = _mapper.Map<SnippetReadDto>(snippet);

            return CreatedAtRoute(nameof(GetSnippetById), new {Id = snippetReadDto.Id}, snippetReadDto);

        }
        
        // We actually hate this
        [HttpPut("{id}")]
        public ActionResult UpdateSnippet(int id, SnippetUpdateDto snippetToUpdate)
        {
            var snippetModelFromRepo = _snippetsRepo.GetSnippetById(id);
            
            if (snippetModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(snippetToUpdate, snippetModelFromRepo);
            
            //_apiRepo.UpdateCommand(cmdModelFromRepo);

            _snippetsRepo.SaveSnippetsChanges();

            // Return 204 (No Content)
            //return NoContent();
            var cmd = new SnippetReadDto{
                Id = id,
                HowTo = snippetToUpdate.HowTo,
                CodeSnippet = snippetToUpdate.CodeSnippet,
                Platform = snippetToUpdate.Platform
            };

            // Return updated Resource
            return Ok(_mapper.Map<SnippetReadDto>(cmd));

        }

        [HttpPatch("{id}")]
        public ActionResult PartialSnippetUpdate(
            int id, 
            JsonPatchDocument<SnippetUpdateDto> patchDoc)
        {
            var snippetModelFromRepo = _snippetsRepo.GetSnippetById(id);
            if (snippetModelFromRepo == null)
            {
                return NotFound();
            }

            var snippetToPatch = _mapper.Map<SnippetUpdateDto>(snippetModelFromRepo);
            patchDoc.ApplyTo(snippetToPatch, ModelState);

            if (!TryValidateModel(snippetToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(snippetToPatch, snippetModelFromRepo);

            _snippetsRepo.UpdateSnippet(snippetModelFromRepo);

            _snippetsRepo.SaveSnippetsChanges();

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
        public ActionResult DeleteSnippet(int id)
        {
            var snippetModelFromRepo = _snippetsRepo.GetSnippetById(id);
            if (snippetModelFromRepo == null)
            {
                return NotFound();
            }
            
            _snippetsRepo.DeleteSnippet(snippetModelFromRepo);
            _snippetsRepo.SaveSnippetsChanges();

            // return NoContent();
            return Ok(_mapper.Map<SnippetReadDto>(snippetModelFromRepo));
        }
    }
}