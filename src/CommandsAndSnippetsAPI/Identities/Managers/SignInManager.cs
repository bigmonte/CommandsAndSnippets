using System;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Data.Cryptography;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public class SignInManager : SignInManager<User>
    {
        private readonly IHasher _hasher;

        public SignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation, IHasher hasher) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes, confirmation)
        {
            _hasher = hasher;
        }

        
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                //var hashedUserName = _hasher.CreateHash(userName, BaseCryptoItem.HashAlgorithm.SHA3_512);
                //_securityLogger.LogEvent(LogLevel.Debug, SecurityEvent.Authentication.USER_NOT_FOUND, $"Login failed because username not found: {hashedUserName}");
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public override async Task<SignInResult> PasswordSignInAsync(User user, string password,
            bool isPersistent, bool lockoutOnFailure)
        {

            var attempt = await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            return attempt.Succeeded
                ? await SignInOrTwoFactorAsync(user, isPersistent)
                : attempt;
        }

        public override async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var error = await PreSignInCheck(user);
            if (error != null)
            {
                return error;
            }

            if (await UserManager.CheckPasswordAsync(user, password))
            {
                var alwaysLockout = AppContext.TryGetSwitch("Microsoft.AspNetCore.Identity.CheckPasswordSignInAlwaysResetLockoutOnSuccess", out var enabled) && enabled;
                // Only reset the lockout when TFA is not enabled when not in quirks mode
                if (alwaysLockout ) // Ignoring Tfa for now
                {
                    await ResetLockout(user);
                }

                return SignInResult.Success;
            }
            Logger.LogWarning(2, "User {userId} failed to provide the correct password.", await UserManager.GetUserIdAsync(user));

            if (UserManager.SupportsUserLockout && lockoutOnFailure)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user);
                if (await UserManager.IsLockedOutAsync(user))
                {
                    return await LockedOut(user);
                }
            }
            return SignInResult.Failed;
        }


        protected override async Task<SignInResult> PreSignInCheck(User user)
        {
            if (user == null)
                return null;

            if (!await CanSignInAsync(user))
            {
                return SignInResult.NotAllowed;
            }
            if (await IsLockedOut(user))
            {
                return await LockedOut(user);
            }
            return null;
        }

    }
}