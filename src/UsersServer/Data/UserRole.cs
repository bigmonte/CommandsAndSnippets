using System;
using Microsoft.AspNetCore.Identity;

namespace UsersServer.Data
{
    public class UserRole : IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}