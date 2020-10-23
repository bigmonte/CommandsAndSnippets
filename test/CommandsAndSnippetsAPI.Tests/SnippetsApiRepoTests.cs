
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
    public class SnippetsApiRepoTests : IDisposable
    {
        private MapperConfiguration _configuration;
        private IMapper _mapper;
        private Mock<ISnippetsAPIRepo> _mockApiRepo = new Mock<ISnippetsAPIRepo>();
        private SnippetsProfile _realProfile = new SnippetsProfile();

        public SnippetsApiRepoTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }


        public void Dispose()
        {
            _mockApiRepo = null;
            _mapper = null;
            _configuration = null;
            _realProfile = null;
        }

        private async Task<IEnumerable<Snippet>> GetSnippetsAsync(int num)
        {
            var snippets = new List<Snippet>();

            for (var i = 0; i < num; i++)
                snippets.Add(new Snippet
                {
                    Id = 0,
                    HowTo = "How to generate migration",
                    CodeSnippet = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });

            return snippets;
        }
        
        private IEnumerable<Snippet> GetSnippets(int num)
        {
            var snippets = new List<Snippet>();

            for (var i = 0; i < num; i++)
                snippets.Add(new Snippet
                {
                    Id = 0,
                    HowTo = "How to generate migration",
                    CodeSnippet = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });

            return snippets;
        }
        

        [Fact]
        public async void GetSnippets_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange

            _mockApiRepo.Setup(repo =>
                repo.GetSnippets()).Returns(GetSnippets(0));

            var controller = new SnippetsController(_mockApiRepo.Object, _mapper);

            // Act

            var result = controller.GetSnippets();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetSnippets_Returns200OK_WhenDBHasOneResource()
        {
            // Arrange

            _mockApiRepo.Setup(repo =>
                repo.GetSnippets()).Returns(GetSnippets(1));

            var controller = new SnippetsController(_mockApiRepo.Object, _mapper);

            // Act

            var result = controller.GetSnippets();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async void GetSnippets_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange
            _mockApiRepo.Setup(repo =>
                repo.GetSnippets()).Returns(GetSnippets(1));
            var controller = new SnippetsController(_mockApiRepo.Object, _mapper);

            // Act 
            var result = controller.GetSnippets();

            // Assert

            var okResult = result.Result as OkObjectResult;

            var snippets = okResult.Value as List<SnippetReadDto>;

            Assert.Single(snippets);
        }

        [Fact]
        public void GetSnippetById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            // Arrange
            _mockApiRepo.Setup(repo => repo.GetSnippetById(0)).Returns(() => null);
            var controller = new SnippetsController(_mockApiRepo.Object, _mapper);

            // Act 
            var result = controller.GetSnippetById(1);

            // Assert

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public void GetSnippetById_Returns200OK_WhenValidIDProvided()
        {
            // Arrange
            _mockApiRepo.Setup(repo => repo.GetSnippetById(0)).Returns(
                new Snippet {Id = 0, HowTo = "Mock", Platform = "MockPL", CodeSnippet = "Mock"});

            var controller = new SnippetsController(_mockApiRepo.Object, _mapper);

            // Act 
            var result = controller.GetSnippetById(0);

            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}