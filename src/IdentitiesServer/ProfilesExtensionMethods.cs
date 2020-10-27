using System;
using AutoMapper;

namespace IdentitiesServer.Profiles
{
    public static class ProfilesExtensionMethods
    {
        public static void TwoWayMapping(this Profile profile, Type type1, Type type2)
        {
            profile.CreateMap(type1, type2);
            profile.CreateMap(type2, type1);
        }
    }
}