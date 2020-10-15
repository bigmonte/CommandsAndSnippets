using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public interface IUserRepo : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task CreateUser(User user);
        public Task SaveChanges();

        public Task<User> GetUserById(string id);

    }
}