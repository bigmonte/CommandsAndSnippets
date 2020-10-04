using System;
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

            Type cmdType = typeof(Command);
            Type cmdReadDto = typeof(CommandReadDto);
            Type cmdCreateDto = typeof(CommandCreateDto);
            Type cmdUpdateDto = typeof(CommandUpdateDto);

            // 1 way mapping
            TwoWayMapping(cmdType, cmdUpdateDto);
            TwoWayMapping(cmdType, cmdReadDto);
            TwoWayMapping(cmdType, cmdCreateDto);

        }
        private void TwoWayMapping(Type type1, Type type2)
        {
            CreateMap(type1, type2);
            CreateMap(type2, type1);
        }
    }
}
