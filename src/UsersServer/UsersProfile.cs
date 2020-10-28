using System;
using AutoMapper;
using UsersServer.Dtos;
using UsersServer.Models;

namespace UsersServer.Profiles
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