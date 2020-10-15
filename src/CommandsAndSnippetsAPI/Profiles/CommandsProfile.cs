using System;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Map our source object (Command) to our target object (CommandReadDto)

            Type cmdType = typeof(Command);
            Type cmdReadDto = typeof(CommandReadDto);
            Type cmdCreateDto = typeof(CommandCreateDto);
            Type cmdUpdateDto = typeof(CommandUpdateDto);

            this.TwoWayMapping(cmdType, cmdUpdateDto);
            this.TwoWayMapping(cmdType, cmdReadDto);
            this.TwoWayMapping(cmdType, cmdCreateDto);

        }
    }
}
