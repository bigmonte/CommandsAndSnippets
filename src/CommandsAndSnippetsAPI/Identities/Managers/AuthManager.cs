using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Dtos.User;
using CommandsAndSnippetsAPI.Identities.Contracts;
using CommandsAndSnippetsAPI.Identities.Cryptography;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Identities.Managers
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

        public async Task <bool> PasswordIsValid(User user, string password)
        {
           var result = await  _signInManager.CheckPasswordSignInAsync(user, password, false);
           if (result.Succeeded) return true;
           return false;
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
        

        public Task<AuthenticationDto> LoginAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }

    }
}