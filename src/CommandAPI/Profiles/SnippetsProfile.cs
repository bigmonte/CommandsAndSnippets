using System;
using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles
{
    public class SnippetsProfile : Profile
    {
        public SnippetsProfile()
        {
            // Map our source object (Command) to our target object (CommandReadDto)

            Type snippetType = typeof(Snippet);
            Type snippetReadDto = typeof(SnippetReadDto);
            Type snippetCreateDto = typeof(SnippetCreateDto);
            Type snippetUpdateDto = typeof(SnippetUpdateDto);

            // 1 way mapping
            TwoWayMapping(snippetType, snippetUpdateDto);
            TwoWayMapping(snippetType, snippetReadDto);
            TwoWayMapping(snippetType, snippetCreateDto);

        }
        private void TwoWayMapping(Type type1, Type type2)
        {
            CreateMap(type1, type2);
            CreateMap(type2, type1);
        }
    }
}
