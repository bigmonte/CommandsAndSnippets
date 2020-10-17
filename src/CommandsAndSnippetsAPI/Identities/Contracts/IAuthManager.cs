using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Dtos.User;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Identities.Contracts
{
    // TODO: Only allow login in the admin frontend 
    public interface IAuthManager
    {
        Task<IdentityResult> CreateUserAsync(UserSignupDto userSignupDto);
        
        Task<bool> LoginAsync(string email, string password);
        
        Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken);
    }
}