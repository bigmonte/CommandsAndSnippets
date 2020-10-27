using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentitiesServer.Dtos.User
{
    public sealed class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}