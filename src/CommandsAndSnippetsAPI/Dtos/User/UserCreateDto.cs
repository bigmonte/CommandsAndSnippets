using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Dtos.User
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}