using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommandsAndSnippetsAPI.Controllers;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Dtos;
using CommandsAndSnippetsAPI.Models;
using CommandsAndSnippetsAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandsAndSnippetsAPI.Tests
{
    public class ApiRepositoryTests : IDisposable
    {
        private Mock<ICommandsAndSnippetsAPIRepo> _mockApiRepo = new Mock<ICommandsAndSnippetsAPIRepo>();
        private CommandsProfile _realProfile = new CommandsProfile();
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public ApiRepositoryTests()
        {
            _configuration = new MapperConfiguration( cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }
        
        private async  Task<List<Command>> GetCommands(int num)
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
        public async void GetCommands_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange
            
            _mockApiRepo.Setup(repo => 
                repo.GetCommands()).Returns(GetCommands(0));
            
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act

            var result = controller.GetCommands().Result;
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }
        [Fact]
        public void GetCommands_Returns200OK_WhenDBHasOneResource()
        {
            // Arrange
            
            _mockApiRepo.Setup(repo => 
                repo.GetCommands()).Returns(GetCommands(1));
            
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act

            var result = controller.GetCommands().Result;
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }
        


        [Fact]
        public async void GetCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange
            _mockApiRepo.Setup(repo => 
                repo.GetCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act 
            var result = await controller.GetCommands();
            
            // Assert

            var okResult = result.Result as OkObjectResult;

            var commands = okResult.Value as List<CommandReadDto>;
            
            Assert.Single(commands);
        }
        
        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            // Arrange
            _mockApiRepo.Setup(repo => repo.GetCommandByIdAsync(0)).Returns(() => null);
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act 
            var result = controller.GetCommandByIdAsync(1).Result;
            
            // Assert

            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        
        [Fact]
        public void GetCommandById_Returns200OK_WhenValidIDProvided()
        {
            // Arrange
            _mockApiRepo.Setup(repo => repo.GetCommandById(0)).Returns(
                new Command {Id = 0, HowTo = "Mock", Platform = "MockPL", CommandLine = "Mock"});
            
            var controller = new CommandsController(_mockApiRepo.Object, _mapper);
            
            // Act 
            var result = controller.GetCommandById(0);
            
            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
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