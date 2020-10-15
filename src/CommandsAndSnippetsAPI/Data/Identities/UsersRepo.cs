using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public class UsersRepo : IUserRepo
    {
        private readonly IdentitiesContext _context;

        public UsersRepo(IdentitiesContext context)
        {
            _context = context;
        }
        private async Task<bool> SaveChanges(User user)
        {
            _context.Users.Update(user);
            
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => true, cancellationToken: cancellationToken);
            
            if (getUser == null)
            {
                throw new Exception(nameof(user));
            }

            return getUser.Id;

        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => true, cancellationToken: cancellationToken);
            
            if (getUser == null)
            {
                throw new Exception(nameof(user));
            }

            return getUser.UserName;
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => x == user, cancellationToken: cancellationToken);
            if (foundUser == null) return;
            foundUser.UserName = userName;
            await SaveChanges(user);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => true, cancellationToken: cancellationToken);
            
            if (getUser == null)
            {
                throw new Exception(nameof(user));
            }

            return getUser.NormalizedUserName;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var u = await _context.AddAsync(user, cancellationToken);
            
            if(u.State == EntityState.Added) return IdentityResult.Success;
           
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            
            
            var success =  await _context.SaveChangesAsync(cancellationToken) > 0;
            
           if (success) return IdentityResult.Success;
           
           return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => true, cancellationToken: cancellationToken);

            var error = new IdentityError {Description = "User Not found"};

            if (foundUser == null) return IdentityResult.Failed(error);
            
            _context.Users.Remove(foundUser);
            
            return IdentityResult.Success;
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}