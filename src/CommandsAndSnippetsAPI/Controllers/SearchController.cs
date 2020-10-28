using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommandsAndSnippetsAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController: ControllerBase
    {
        private readonly ICommandsApiRepo _commandsApiRepo;
        private readonly ISnippetsAPIRepo _snippetsApiRepo;
        private readonly IMapper _mapper;
        
        public SearchController(
            ICommandsApiRepo commandsApiRepo, 
            ISnippetsAPIRepo snippetsApiRepo,
            IMapper mapper)
        {
            _commandsApiRepo = commandsApiRepo;
            _snippetsApiRepo = snippetsApiRepo;
            _mapper = mapper;
        }

        
        [HttpGet("commands/platform/{platform}", Name = "CommandsWithPlatform")]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> CommandsWithPlatform(string platform)
        {
            var commandsWithPlatform = await _commandsApiRepo.GetCommandsWithPlatform(platform);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsWithPlatform));
        }
        
        
        [HttpGet("commands/{text}")]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> SearchCommand(string text)
        {
            var searchResult = await _commandsApiRepo.SearchCommands(text);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(searchResult));
        }
        
        
        [HttpGet("snippets/{text}")]
        public async Task<ActionResult<IEnumerable<SnippetReadDto>>> SearchSnippet(string text)
        {
            var searchResult = await _snippetsApiRepo.SearchSnippets(text);
            return Ok(_mapper.Map<IEnumerable<SnippetReadDto>>(searchResult));
        }
        
        

    }
}