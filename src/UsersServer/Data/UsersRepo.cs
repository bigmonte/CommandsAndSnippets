using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersServer.Identities.Contracts;
using UsersServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersServer.Data.Identities
{
    public class UsersRepo : UserStore<User>, IUserRepo
    {
        private readonly UserDbContext _context;

        public UsersRepo(UserDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
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

        public override Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.Id);

        }

        public override Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.UserName);
        }

        public override async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken: cancellationToken);
            if (foundUser == null) return;
            foundUser.UserName = userName;
            await SaveChanges(user);
        }

        public override  Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.NormalizedUserName);

        }

        public override async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var u = await  _context.AddAsync(user, cancellationToken);

            if(u.State == EntityState.Added)
            {
                await SaveChanges();
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public override async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);

            var success =  await _context.SaveChangesAsync(cancellationToken) > 0;
            
           if (success) return IdentityResult.Success;
           
           return IdentityResult.Failed();
        }

        public override async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x=> x.Id == user.Id, cancellationToken: cancellationToken);

            var error = new IdentityError {Description = "User Not found"};

            if (foundUser == null) return IdentityResult.Failed(error);
            
            _context.Users.Remove(foundUser);
            
            return IdentityResult.Success;
        }

        public async Task<User> GetUserById(string id)
        {
            var db = _context.Users;
            var user = await db.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public override Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.PasswordHash);
        }

        public override Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
  
        public override Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.Email);
        }

        public override Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public override async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            User user = null;
            var db = _context.Users;
            user = await db.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);
            return user;
        }

        public override Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            base.GetNormalizedEmailAsync(user, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<string>(cancellationToken);
            }

            return Task.FromResult(user.NormalizedEmail);
        }


        public UsersRepo(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}