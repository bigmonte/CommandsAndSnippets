using System.Threading.Tasks;
using IdentitiesServer.Dtos.User;
using IdentitiesServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentitiesServer.Identities.Contracts
{
    // TODO: Only allow login in the admin frontend 
    public interface IAuthManager
    {
        Task<IdentityResult> CreateUserAsync(UserSignupDto userSignupDto);
        
        Task<bool> LoginAsync(string email, string password);
        
        Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken);
    }
}