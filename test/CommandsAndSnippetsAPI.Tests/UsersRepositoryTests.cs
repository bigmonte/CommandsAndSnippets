using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Controllers;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Data.Identities;
using CommandsAndSnippetsAPI.Models;
using CommandsAndSnippetsAPI.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CommandsAndSnippetsAPI.Tests
{
    public class UsersRepositoryTests : IDisposable
    {
        private Mock<IUserRepo> _mockApiRepo = new Mock<IUserRepo>();
        private UsersProfile _realProfile = new UsersProfile();
        
        private MapperConfiguration _configuration;
        private IMapper _mapper;
        private UsersController _controller;
        private Mock<UserManager> _mockUserManager;
        private Mock<SignInManager> _mockSignInManager;
        public UsersRepositoryTests()
        {
            _configuration = new MapperConfiguration( cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
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
        public async void UserWithPasswordHasHashVerified()
        {
            // Arrange

       
            var firstUser = GetUsers(1).First();

            // Act

            var result = await _mockSignInManager.Object.PasswordSignInAsync(firstUser, "password", false, false);
            // Assert
            Assert.IsType<SignInResult>(result);

        }
        
        public void Dispose()
        {
            
        }
    }
}