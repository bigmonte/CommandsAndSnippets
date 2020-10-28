using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UsersServer.Models;

namespace UsersServer.Contracts
{
    /// <summary>
    /// This is our Users repository.
    /// Since this is a simple app we'll have the following roles
    /// Admin and APIClient
    /// </summary>
    
    public interface IUserRepo : IUserStore<User>
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task CreateUser(User user);
        public Task SaveChanges();

        public Task<User> GetUserById(string id);

        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
    }
}