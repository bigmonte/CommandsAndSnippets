using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Dtos.User
{
    public sealed class UserSignupDto
    {
        [Required] [MaxLength(250)] [MinLength(3)]
        public string UserName { get; init; }
        
        [EmailAddress] [Required]
        public string Email { get; init; }
        
        [Required]
        public string Password { get; init; }
    }
}