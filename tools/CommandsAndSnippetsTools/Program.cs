using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndSnippetsTools.Tools;

namespace CommandsAndSnippetsTools
{
    internal class Program
    {
        private enum RegisteredArgs { XmlCommandsDump, Print}
        private static void Main(string[] args)
        {
            Dictionary<RegisteredArgs, string> registeredArgs = new Dictionary<RegisteredArgs, string>
            {
                {RegisteredArgs.XmlCommandsDump, "xml-cmds-dump"},
                {RegisteredArgs.Print, "print"}

            };
            
            var hasRegisteredArg = hasValidArgument(args, registeredArgs);

            if (!hasRegisteredArg)
            {
                PrintHelp();
                return;
            }

            if (args[0] == registeredArgs[RegisteredArgs.XmlCommandsDump])
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
            
            if (args[0] == registeredArgs[RegisteredArgs.Print]) PrintTests();

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
        
        private static void PrintHelp()
        {
            Console.WriteLine("Command                                 |   Description");
            Console.WriteLine();
            Console.WriteLine("dotnet run xml-cmds-dump <full-path>    |   Generate Commands XML Dump");
            Console.WriteLine("dotnet print                            |   Print tests");
        }
        private static bool hasValidArgument(string[] args, Dictionary<RegisteredArgs, string> registeredArgs)
        {
            bool hasRegisteredArg = false;
            
            foreach (var registeredArg in registeredArgs.Values)
            {
                if (args.Any(arg => arg == registeredArg))
                {
                    hasRegisteredArg = true;
                }
            }

            return hasRegisteredArg;
        }

    }
}