using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsAndSnippetsAPI.Controllers;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Data.Cryptography;
using CommandsAndSnippetsAPI.Data.Identities;
using CommandsAndSnippetsAPI.Models;
using CommandsAndSnippetsAPI.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CommandsAndSnippetsAPI.Tests
{
    public class UsersRepositoryTests : IDisposable
    {
        private readonly Hasher _hasher;
        private readonly Mock<IUserRepo> _mockApiRepo = new Mock<IUserRepo>();
        private readonly Mock<ILoginManager> _mockLoginManager = new Mock<ILoginManager>();
        private readonly Mock<IHasher> _mockIHasher = new Mock<IHasher>();
        
        private readonly UsersProfile _realProfile = new UsersProfile();
        
        private MapperConfiguration _configuration;
        private IMapper _mapper;
        private UsersController _controller;
        private Mock<SignInManager> _mockSignInManager = new Mock<SignInManager>();
        public UsersRepositoryTests()
        {
            _configuration = new MapperConfiguration( cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
            _hasher = new Hasher();
            
            // Todo handle me 
            
            var mockUserManager = new Mock<UserManager>(_mockApiRepo.Object as IUserStore<User>,
                null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            _mockSignInManager = new Mock<SignInManager>(mockUserManager.Object as UserManager<User>,
                contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null, _mockIHasher.Object);
 
            _controller = new UsersController(_mapper, _mockApiRepo.Object,_mockLoginManager.Object);
        }
        
        
        private  List<User> GetUsers(int num)
        {
            var users = new List<User>();

            for (int i = 0; i < num; i++)
            {
                users.Add( new User
                {
                    Id = "MOCKID",
                    AccessFailedCount =  0,
                    ConcurrencyStamp =  "",
                    Email = "mockemail@email.com",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = "",
                    NormalizedUserName = "",
                    PasswordHash = "[3]263AA36D34715C54B5E380EE1274F010621D27378EFDDE4DDC0E4CD762ADE456599B03330B7F0F076CB2EFEEF7F8BDC11A9EB248411A30B7D91B2B3988E6D5E92F621FC78138F132BD70F19A2873BB45E34161C58C2ABD38E7DAC52B1A0C8B39"
                });
            }

            return users;
        }
        
        [Fact]
        public  void SHA3_512_Password_Gets_Verified()
        {

            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("password", password);

            // Assert
            
            Assert.True(result);

        }
        
        [Fact]
        public void SHA2_512_Password_Gets_Verified()
        {
            // Arrange
            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA2_512);

            // Act
            var result = _hasher.MatchesHash("password", password);

            // Assert
            
            Assert.True(result);

        }
        
        [Fact]
        public void SHA3_512_Password_With_Symbols_and_Numbers_Gets_Verified()
        {
            // Arrange
            
            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtUxo912=27%$//dUwuq", password);

            // Assert
            
            Assert.True(result);

        }
        
        
        [Fact]
        public void SHA3_512_Password_With_Different_Symbols_and_Numbers_Gets_Not_Verified()
        {
            // Arrange
            
            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtU1291212191dUwuq", password);

            // Assert
            
            Assert.False(result);

        }
        
        
        
        
        
        
        public void Dispose()
        {
            
        }
    }
}