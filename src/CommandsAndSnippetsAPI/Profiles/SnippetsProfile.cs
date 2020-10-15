using System;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Profiles
{
    public class SnippetsProfile : Profile
    {
        public SnippetsProfile()
        {
            Type snippetType = typeof(Snippet);
            Type snippetReadDto = typeof(SnippetReadDto);
            Type snippetCreateDto = typeof(SnippetCreateDto);
            Type snippetUpdateDto = typeof(SnippetUpdateDto);

            this.TwoWayMapping(snippetType, snippetUpdateDto);
            this.TwoWayMapping(snippetType, snippetReadDto);
            this.TwoWayMapping(snippetType, snippetCreateDto);

        }
    }
}
