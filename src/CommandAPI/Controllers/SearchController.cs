using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController: ControllerBase
    {
        private readonly ICommandAPIRepo _apiRepo;
        private readonly IMapper _mapper;
        
        public SearchController(ICommandAPIRepo apiRepo, IMapper mapper)
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