using System;
using CommandsAndSnippetsTools.Tools;

namespace CommandsAndSnippetsTools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Command                                 |   Description");
                Console.WriteLine();
                Console.WriteLine("dotnet run xml-cmds-dump <full-path>    |   Generate Commands XML Dump");
                Console.WriteLine("dotnet print                            |   Print tests");
                return;
            }
            
            if (args[0] == "xml-dump")
            {
                if (args.Length > 1)
                {
                    // Second parameter is the path to save the XML Dump
                    GenerateXmlDump(args[1]);
                    return;
                }

                GenerateXmlDump();
                return;
            }
            
            if (args[0] == "print" || args[0] == "print-tests") PrintTests();

        }

        private static void GenerateXmlDump(string path = "")
        {
            var generator = new XmlDumpTool();
            generator.CommandsDump(path);
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