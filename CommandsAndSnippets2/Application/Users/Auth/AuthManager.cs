using System.Security.Claims;
using BlueWest.Cryptography;
using  CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using static   CommandsAndSnippets2.Users.AuthConsts;

namespace   CommandsAndSnippets2.Users
{
    /// <summary>
    /// Authentication Manager for the Application Users
    /// </summary>
    public class AuthManager : IAuthManager
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IHasher _hasher;

        /// <summary>
        /// Auth manager constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="hasher"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="sessionCache"></param>
        public AuthManager(
            ApplicationUserManager userManager,
            IHasher hasher)
        {
            _userManager = userManager;
            _hasher = hasher;
        }

   

        private string GetHashFromUuid(string uuid)
        {
            return _hasher.CreateHash(uuid, BaseCryptoItem.HashAlgorithm.SHA2_512);
        }
        
        
        /// <summary>
        /// Verify Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> VerifyLoginByEmailAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }


        /// <summary>
        /// Create user 
        /// </summary>
        /// <param name="userSignupDto"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateUserAsync(SignUpRequest userSignupDto)
        {
            userSignupDto.Password = _hasher.CreateHash(userSignupDto.Password, BaseCryptoItem.HashAlgorithm.SHA3_512);;
            var newUser = userSignupDto.ToUser();
            return await _userManager.CreateAsync(newUser);
        }

        public async Task<(ClaimsIdentity, bool)> DoLogin(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null) return NegativeToken;

            if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password)) return NegativeToken;
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            
            //identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));

            return (identity, true);
        }
    }
}