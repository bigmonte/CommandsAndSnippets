using System;
using AutoMapper;
using IdentitiesServer.Dtos;
using IdentitiesServer.Dtos.User;
using IdentitiesServer.Models;

namespace IdentitiesServer.Profiles
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