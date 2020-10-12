using System;

namespace CommandsAndSnippetsTools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Help: ");
                Console.WriteLine("Command                 Description");
                Console.WriteLine("--------------------|--------------");
                Console.WriteLine("dotnet run xml-dump |   Generate Commands XML Dump");
                Console.WriteLine("dotnet print        |   Print tests");
            }

            foreach (var arg in args)
            {
                if (arg == "xml-dump") GenerateXmlDump();

                if (arg == "print" || arg == "print-tests") PrintTests();
            }
        }

        private static void GenerateXmlDump()
        {
            var generator = new GenerateXmlFromDb();
            generator.Generate();
        }

        private static void PrintTests()
        {
            var printTests = new PrintTests();

            Console.WriteLine($"{nameof(printTests.PrintFirstItem)} ↓");
            printTests.PrintFirstItem();

            Console.WriteLine("");

            Console.WriteLine($"{nameof(printTests.PrintResultsWithEfPlatform)} ↓");
            printTests.PrintResultsWithEfPlatform();
        }
    }
}