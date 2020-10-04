using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Map our source object (Command) to our target object (CommandReadDto)
            
            CreateMap<Command, CommandReadDto>();
        }
    }
}
