using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests
{
    public class ApiRepositoryTests : IDisposable
    {
        private Mock<ICommandAPIRepo> _mockApiRepo = new Mock<ICommandAPIRepo>();
        private CommandsProfile _realProfile = new CommandsProfile();
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public ApiRepositoryTests()
        {
            _configuration = new MapperConfiguration( cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }

        [Fact]
        public void GetCommands_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange
            
            _mockApiRepo.Setup(repo => 
                repo.GetCommands()).Returns(GetCommands(0));
            
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act

            var result = controller.GetCommands();
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();

            for (int i = 0; i < num; i++)
            {
                commands.Add( new Command
                {
                    Id = 0,
                    HowTo = "How to generate migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }

            return commands;
        }

        [Fact]
        public void GetCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange
            _mockApiRepo.Setup(repo => 
                repo.GetCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act 
            var result = controller.GetCommands();
            
            // Assert

            var okResult = result.Result as OkObjectResult;

            var commands = okResult.Value as List<CommandReadDto>;
            
            Assert.Single(commands);
        }
        

        public void Dispose()
        {
            _mockApiRepo = null;
            _mapper = null;
            _configuration = null;
            _realProfile = null;
        }
    }
}