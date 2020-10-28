using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UsersServer.Dtos;

namespace UsersServer.Contracts
{
    public interface IAuthManager
    {
        Task<IdentityResult> CreateUserAsync(UserSignupDto userSignupDto);
        
        Task<bool> LoginAsync(string email, string password);

        Task<AccessToken> GetToken(UserLoginDto message);

    }
}