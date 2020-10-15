using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Dtos.User
{
    public class UserSignupDto
    {
        [Required] [MaxLength(250)] [MinLength(3)]
        public string UserName { get; set; }
        
        [EmailAddress] [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}