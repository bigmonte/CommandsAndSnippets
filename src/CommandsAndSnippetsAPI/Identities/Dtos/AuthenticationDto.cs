using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Dtos.User
{
    public sealed class AuthenticationDto
    {
        public string Token { get; init; }
        
        public IdentityResult IdentityResult { get; init; }
    }
}