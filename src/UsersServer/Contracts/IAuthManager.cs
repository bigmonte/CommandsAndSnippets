using System.Threading.Tasks;
using UsersServer.Dtos.User;
using UsersServer.Models;
using Microsoft.AspNetCore.Identity;

namespace UsersServer.Identities.Contracts
{
    // TODO: Only allow login in the admin frontend 
    public interface IAuthManager
    {
        Task<IdentityResult> CreateUserAsync(UserSignupDto userSignupDto);
        
        Task<bool> LoginAsync(string email, string password);
        
        Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken);
    }
}