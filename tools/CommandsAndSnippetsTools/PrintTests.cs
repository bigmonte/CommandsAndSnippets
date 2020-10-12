using System;
using System.Linq;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsTools
{
    public class PrintTests
    {
        private readonly DBContext _dbContext;

        public PrintTests()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void PrintFirstItem()
        {
            Console.WriteLine($"{_dbContext.CommandItems.First()?.Platform}");

        }

        public void PrintResultsWithEfPlatform()
        {
            var query = from c in _dbContext.CommandItems // LINQ to Objects
                where c.Platform == "Entity Framework CLI"
                select c;
            
            foreach (var command in query)
            {
                Console.WriteLine($"Platform: {command.Platform}, " +
                                  $"How To: {command.HowTo}, " +
                                  $"Command: {command.CommandLine}");
            }
        }
    }
}