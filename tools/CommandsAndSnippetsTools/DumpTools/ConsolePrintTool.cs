using System;
using System.Linq;
using CommandsAndSnippetsAPI.Data;

namespace CommandsAndSnippetsTools.Tools
{
    public class ConsolePrintTool
    {
        private readonly ApiDataContext _dbContext;

        public ConsolePrintTool()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void PrintFirstItem()
        {
            Console.WriteLine("Going to print first item...");
            Console.WriteLine($"{_dbContext.CommandItems.First()?.Platform}");
        }

        public void PrintResultsWithEfPlatform()
        {
            Console.WriteLine("Commands with Entity Framework platform...");

            var query = from c in _dbContext.CommandItems // LINQ to Objects
                where c.Platform == "Entity Framework CLI"
                select c;

            foreach (var command in query)
                Console.WriteLine($"Platform: {command.Platform}, " +
                                  $"How To: {command.HowTo}, " +
                                  $"Command: {command.CommandLine}");
        }
    }
}