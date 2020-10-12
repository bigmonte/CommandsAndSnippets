using System;

namespace CommandsAndSnippetsTools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GenerateXmlDump();
            /*if (args.Length == 0)
            {
                Console.WriteLine("Command             |   Description");
                Console.WriteLine("--------------------|--------------");
                Console.WriteLine("dotnet run xml-dump |   Generate Commands XML Dump");
                Console.WriteLine("dotnet print        |   Print tests");
                return;
            }
            
            if (args[0] == "xml-dump")
            {
                GenerateXmlDump();
                return;
            }
            
            if (args[0] == "print" || args[0] == "print-tests") PrintTests();*/

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