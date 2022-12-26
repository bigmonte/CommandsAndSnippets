
using BlueWest.Cryptography;
using CommandsAndSnippets2.Application;
using  CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace  CommandsAndSnippets2.Users;

/// <summary>
/// User Manager Object
/// </summary>
public class ApplicationUserManager : UserManager<ApplicationUser>, IUserManager
{
        private readonly IHasher _hasher;
        private readonly UserRepository _usersRepo;
        public ApplicationUserManager(
            UserRepository store, 
            IOptions<IdentityOptions> optionsAccessor,
            IHasher passwordHasher, 
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _hasher = passwordHasher;
            _usersRepo = store;
        }

        public override async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();

            var result = await VerifyPasswordAsync(passwordStore, user, password);
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                //Remove the IPasswordStore parameter so we can call the protected, not private, method
                await UpdatePasswordHash(user, password, validatePassword: false);
                await UpdateUserAsync(user);
            }

            var success = result != PasswordVerificationResult.Failed;
            if (!success)
            {
                var userId = user != null ? GetUserIdAsync(user).Result : "(null)";
                Logger.LogWarning(0, "Invalid password for user {userId}.", userId);
            }
            return success;
        }


        protected override async Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<ApplicationUser> store, ApplicationUser user, string password)
        {
            string existingHash;

            if (user != null)
                existingHash = await store.GetPasswordHashAsync(user, CancellationToken);
            else
                existingHash = "not a real hash";

            if (existingHash == null)
            {
                return PasswordVerificationResult.Failed;
            }
            return PasswordHasher.VerifyHashedPassword(user, existingHash, password);
        }

       

        public override async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await VerifyPasswordAsync(passwordStore, user, currentPassword) != PasswordVerificationResult.Failed)
            {
                var result = await UpdatePasswordHash(user, newPassword, validatePassword: false);
                if (!result.Succeeded)
                {
                    return result;
                }

                return await UpdateUserAsync(user);
            }
            Logger.LogWarning(2, "Change password failed for user {userId}.", await GetUserIdAsync(user));
            return IdentityResult.Failed(ErrorDescriber.PasswordMismatch());
        }

        public override Task<IdentityResult> SetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName, string tokenValue)
        {
            return base.SetAuthenticationTokenAsync(user, loginProvider, tokenName, tokenValue);
        }

        private IUserPasswordStore<ApplicationUser> GetPasswordStore()
        {
            if (Store is IUserPasswordStore<ApplicationUser> passwordStore)
            {
                return passwordStore;
            }

            return null;
        }

}