using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UsersServer.Models;

namespace UsersServer.Contracts
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        
        Task<User> FindByEmailAsync(string email);
    }
}