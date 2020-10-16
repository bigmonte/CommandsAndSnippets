using System;
using System.Collections.Generic;
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

        private async Task<bool> SaveChanges(User user)
        {
            var d = _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region IUserStore
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

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var u =  _context.AddAsync(user, cancellationToken).Result;
            
            if(u.State == EntityState.Added) return Task.FromResult(IdentityResult.Success);
           
            return Task.FromResult(IdentityResult.Failed());
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
        
        public async Task<User> GetUserById(string id)
        {
            var db = _context.Users;
            var user = await db.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        
        #endregion

        #region IUserPasswordStore
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
        
        #endregion

        #region EmailStore
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        
        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        public Task SetTokenAsync(User user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}