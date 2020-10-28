using System.Threading.Tasks;
using AutoMapper;
using UsersServer.Dtos.User;
using UsersServer.Identities.Contracts;
using UsersServer.Identities.Cryptography;
using UsersServer.Models;
using Microsoft.AspNetCore.Identity;

namespace UsersServer.Identities.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;
        private readonly IHasher _hasher;
        private readonly IMapper _mapper;
        
        public AuthManager(SignInManager signInManager, UserManager userManager, IHasher hasher, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hasher = hasher;
            _mapper = mapper;
        }
        
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false; // return error user doesn't exist
            }

            return await _userManager.CheckPasswordAsync(user, password);
            
            // return await GenerateAuthenticationResultForUserAsync(user);
        }

        private  UserCreateDto FromSignupToUser(UserSignupDto signupDto)
        {
            var pwd = signupDto.Password;
            var hash = _hasher.CreateHash(pwd, BaseCryptoItem.HashAlgorithm.SHA3_512);

            UserCreateDto userToCreate = new UserCreateDto
            {
                Email = signupDto.Email,
                PasswordHash = hash,
                UserName = signupDto.UserName
            };

            return userToCreate;
        }
        
        public async Task<IdentityResult> CreateUserAsync(UserSignupDto userSignupDto)
        {
            UserCreateDto userToCreate = FromSignupToUser(userSignupDto);
            
            var userMdl = _mapper.Map<User>(userToCreate);
            
            if(userMdl == null) return IdentityResult.Failed(new IdentityError
            {
                Code = "1",
                Description = "An error occured processing your request"
            });
            
            return await _userManager.CreateAsync(userMdl);
        }
        
        
        public Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }

    }
}