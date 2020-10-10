using System.Collections.Generic;
using AutoMapper;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsAndSnippetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController: ControllerBase
    {
        private readonly ICommandsAndSnippetsAPIRepo _apiRepo;
        private readonly IMapper _mapper;
        
        public SearchController(ICommandsAndSnippetsAPIRepo apiRepo, IMapper mapper)
        {
            _apiRepo = apiRepo;
            _mapper = mapper;
        }

        
        [HttpGet("platform/{platform}", Name = "CommandsWithPlatform")]
        public ActionResult<IEnumerable<CommandReadDto>> CommandsWithPlatform(string platform)
        {
            var commandsWithPlatform = _apiRepo.GetCommandsWithPlatform(platform);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsWithPlatform));
        }

    }
}