using System;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos;
using CommandsAndSnippetsAPI.Dtos.User;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Profiles
{
    public class UsersProfile: Profile
    {
        public UsersProfile()
        {
            Type cmdType = typeof(User);
            Type cmdReadDto = typeof(UserReadDto);
            Type cmdCreateDto = typeof(UserCreateDto);

            this.TwoWayMapping(cmdType, cmdReadDto);
            this.TwoWayMapping(cmdType, cmdCreateDto);
        }
    }
}