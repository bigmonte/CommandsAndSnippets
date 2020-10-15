using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public class UsersRepo : IUserRepo, IUserPasswordStore<User>
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
            _context.Dispose();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.Id);

        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.UserName);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken: cancellationToken);
            if (foundUser == null) return;
            foundUser.UserName = userName;
            await SaveChanges(user);
        }

        public  Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.NormalizedUserName);

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
            var foundUser = await _context.Users.FirstOrDefaultAsync(x=> x.Id == user.Id, cancellationToken: cancellationToken);

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

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task CreateUser(User user)
        {
            await CreateAsync(user, CancellationToken.None);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            var db = _context.Users;
            var user = await db.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}