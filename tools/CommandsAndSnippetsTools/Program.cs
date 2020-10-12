using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndSnippetsTools.XmlTools;
using CommandsAndSnippetsTools.Tools;

namespace CommandsAndSnippetsTools
{
    internal sealed class Program
    {
        private enum RegisteredArgs
        {
            XmlCommandsDump,
            Print,
            ReadXmlData,
            Reflection
        }

        private static void Main(string[] args)
        {
            Dictionary<RegisteredArgs, string> registeredArgs = new Dictionary<RegisteredArgs, string>
            {
                {RegisteredArgs.XmlCommandsDump, "xml-dump"},
                {RegisteredArgs.Print, "print"},
                {RegisteredArgs.ReadXmlData, "read-xml"},
                {RegisteredArgs.Reflection, "reflection"}
            };

            var hasRegisteredArg = HasValidArgument(args, registeredArgs);

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

            if (args[0] == registeredArgs[RegisteredArgs.ReadXmlData])
            {
                var xmlTests = new XmlTests();
                Console.WriteLine($"{nameof(xmlTests.ReadXmlData)} ↓");
                xmlTests.ReadXmlData();

                Console.WriteLine($"{nameof(xmlTests.ReadXmlDataAnonymousObj)} ↓");
                xmlTests.ReadXmlDataAnonymousObj();
            }

            if (args[0] == registeredArgs[RegisteredArgs.Reflection])
            {
                var reflection = new ReflectionTests();
                reflection.ReadCommandProperties();
            }
        }

        private static void GenerateXmlDump(string path = "")
        {
            var generator = new XmlDumpTool();
            generator.DumpCommands(path);
            generator.DumpCommandsWithEfPlatform(path);
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
            Console.WriteLine("Commands And Snippets Tools");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("Arguments                        |   Description");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("xml-dump <optional-full-path>    |   Generate Commands XML Dumps from database");
            Console.WriteLine("print                            |   Print tests");
            Console.WriteLine("read-xml                         |   Print XML Tests");
            Console.WriteLine("reflection                       |   Print Reflection Tests");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("Please provide an valid argument!");
        }

        private static bool HasValidArgument(string[] args, Dictionary<RegisteredArgs, string> registeredArgs)
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