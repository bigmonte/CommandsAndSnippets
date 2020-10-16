using System;
using System.Collections.Generic;
using System.Security.Claims;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Data.Cryptography;
using CommandsAndSnippetsAPI.Data.Identities;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CommandsAndSnippetsAPI.Tests
{
    public class PasswordTests : IDisposable
    {
        private readonly Hasher _hasher;
        private readonly Mock<IUserRepo> _mockApiRepo = new Mock<IUserRepo>();

        private readonly SignInManager _signInManager;

        public PasswordTests()
        {
            // Mock Objects
            var mockUserManager = new Mock<UserManager>(_mockApiRepo.Object as IUserStore<User>,
                null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var logger = new Mock<ILogger<SignInManager>>();

            // Testing targets

            IHasher iHasher = new Hasher();
            _hasher = new Hasher();

            _signInManager = new SignInManager(mockUserManager.Object, contextAccessor.Object,
                userPrincipalFactory.Object, null, logger.Object, null, null, iHasher);
        }


        private List<User> GetUsers(int num)
        {
            var users = new List<User>();

            for (int i = 0; i < num; i++)
            {
                users.Add(new User
                {
                    Id = "MOCKID",
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "",
                    Email = "mockemail@email.com",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = "",
                    NormalizedUserName = "",
                    PasswordHash =
                        "[3]263AA36D34715C54B5E380EE1274F010621D27378EFDDE4DDC0E4CD762ADE456599B03330B7F0F076CB2EFEEF7F8BDC11A9EB248411A30B7D91B2B3988E6D5E92F621FC78138F132BD70F19A2873BB45E34161C58C2ABD38E7DAC52B1A0C8B39"
                });
            }

            return users;
        }

        [Fact]
        public void SHA3_512_Password_Gets_Verified()
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
        public void SHA3_512_Password_With_Symbols_and_Numbers_Gets_Success()
        {
            // Arrange

            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtUxo912=27%$//dUwuq", password);

            // Assert

            Assert.True(result);
        }


        [Fact]
        public void SHA3_512_Password_With_Different_Symbols_and_Numbers_Gets_Unsuccess()
        {
            // Arrange

            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtU1291212191dUwuq", password);

            // Assert

            Assert.False(result);
        }


        [Fact]
        public void SignInManager_Gets_LoginSuccess_With_Correct_SHA3512_Hashed_Password()
        {
            // Arrange

            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA3_512);

            User testUser = new User();
            testUser.PasswordHash = password;
            // Act
            var result = _signInManager.CheckPasswordSignInAsync(testUser, "password", false).Result;
            // Assert

            Assert.True(result == SignInResult.Success);
        }


        [Fact]
        public void SignInManager_Gets_LoginFailed_With_Incorrect_SHA3512_Hashed_Password()
        {
            // Arrange

            var password = _hasher.CreateHash("password21d", BaseCryptoItem.HashAlgorithm.SHA3_512);

            User testUser = new User();
            testUser.PasswordHash = password;

            // Act
            var result = _signInManager.CheckPasswordSignInAsync(testUser, "password", false).Result;
            // Assert

            Assert.True(result == SignInResult.Failed);
        }
        [Fact]
        public void SignInManager_Gets_LoginSuccess_With_Correct_SHA2_512_Hashed_Password()
        {
            // Arrange

            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA2_512);

            User testUser = new User();
            testUser.PasswordHash = password;
            // Act
            var result = _signInManager.CheckPasswordSignInAsync(testUser, "password", false).Result;
            // Assert

            Assert.True(result == SignInResult.Success);
        }


        [Fact]
        public void SignInManager_Gets_LoginFailed_With_Incorrect_SHA2_512_Hashed_Password()
        {
            // Arrange

            var password = _hasher.CreateHash("password21d", BaseCryptoItem.HashAlgorithm.SHA2_512);

            User testUser = new User();
            testUser.PasswordHash = password;

            // Act
            var result = _signInManager.CheckPasswordSignInAsync(testUser, "password", false).Result;
            // Assert

            Assert.True(result == SignInResult.Failed);
        }
        
        
        [Fact]
        public void SignInManager_Capable_Of_Verifying_Correct_Passwords_Hashed_With_Different_Algos()
        {
            // Arrange

            var password1 = _hasher.CreateHash("password21d", BaseCryptoItem.HashAlgorithm.SHA3_512);
            var password2 = _hasher.CreateHash("djSasOjd%12$78", BaseCryptoItem.HashAlgorithm.SHA2_512);

            User testUser1 = new User();
            testUser1.PasswordHash = password1;
            
            User testUser2 = new User();
            testUser2.PasswordHash = password2;

            // Act
            var result1 = _signInManager.CheckPasswordSignInAsync(testUser1, "password21d", false).Result;
            var result2 = _signInManager.CheckPasswordSignInAsync(testUser2, "djSasOjd%12$78", false).Result;
            
            // Assert

            Assert.True(result1 == SignInResult.Success );
        }
        
        
        [Fact]
        public void SignInManager_Capable_Of_Verifying_One_Correct_Pwd_And_One_incorrect_pwd_Hashed_With_Different_Algos()
        {
            // Arrange

            var password1 = _hasher.CreateHash("password21d", BaseCryptoItem.HashAlgorithm.SHA3_512);
            var password2 = _hasher.CreateHash("djSasOjd%12$78", BaseCryptoItem.HashAlgorithm.SHA2_512);

            User testUser1 = new User();
            testUser1.PasswordHash = password1;
            
            User testUser2 = new User();
            testUser2.PasswordHash = password2;

            // Act
            var result1 = _signInManager.CheckPasswordSignInAsync(testUser1, "password21d", false).Result;
            var result2 = _signInManager.CheckPasswordSignInAsync(testUser2, "djSasOjd", false).Result;
            
            // Assert

            Assert.True(result1 == SignInResult.Success && result2 == SignInResult.Failed);
        }
        
        
        
        


        public void Dispose()
        {
        }
    }
}