using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    /// <summary>
    /// This is our Users repository.
    /// Since this is a simple app we'll have the following roles
    /// Admin and APIClient
    /// </summary>
    
    public interface IUserRepo : IUserStore<User>, 
        IUserPasswordStore<User>,
        IUserEmailStore<User>, 
        IUserLoginStore<User>, IUserAuthenticationTokenStore<User>
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task CreateUser(User user);
        public Task SaveChanges();

        public Task<User> GetUserById(string id);

    }
}