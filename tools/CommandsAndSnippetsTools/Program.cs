using System;
using System.Linq;
using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace CommandsAndSnippetsTools
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Help: ");
                System.Console.WriteLine("Command                 Description");
                System.Console.WriteLine("--------------------|--------------");
                System.Console.WriteLine("dotnet run xml-dump |   Generate Commands XML Dump");
                System.Console.WriteLine("dotnet print        |   Print tests");

            }
            
            foreach (var arg in args)
            {
                if (arg == "xml-dump")
                {
                    GenerateXmlDump();
                }

                if (arg == "print" || arg == "print-tests")
                {
                    PrintTests();
                }
            }
        }

        static void GenerateXmlDump()
        {
            var generator = new GenerateXmlFromDb();
            generator.Generate();
        }
        
        static void PrintTests()
        {
            PrintTests printTests = new PrintTests();
            
            Console.WriteLine($"{nameof(printTests.PrintFirstItem)} ↓");
            printTests.PrintFirstItem();
            
            Console.WriteLine("");
            
            Console.WriteLine($"{nameof(printTests.PrintResultsWithEfPlatform)} ↓");
            printTests.PrintResultsWithEfPlatform();
        }


    }
}