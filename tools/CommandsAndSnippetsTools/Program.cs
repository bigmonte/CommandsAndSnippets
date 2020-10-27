using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndSnippetsTools.DumpTools;
using CommandsAndSnippetsTools.Tools;
using IdentitiesServerTools;

namespace CommandsAndSnippetsTools
{
    internal sealed class Program
    {
        private enum RegisteredArgs
        {
            XmlCommandsDump,
            Print,
            ReadXmlData,
            Reflection,
            Json,
            Game,
            Crypto
        }
        
        private static void ProgramHelp()
        {
            Console.WriteLine("Commands And Snippets Tools");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("Arguments                        |   Description");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("xml-dump <optional-full-path>    |   Generate Commands XML test dumps");
            Console.WriteLine("print                            |   Print tests");
            Console.WriteLine("read-xml                         |   Print XML Tests");
            Console.WriteLine("reflection                       |   Print Reflection Tests");
            Console.WriteLine("json                             |   Print Json Tests");
            Console.WriteLine("game                             |   Run learning game");
            Console.WriteLine("crypto                           |   Run Crypto tests");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("Please provide an valid argument!");
        }

        private static void Main(string[] args)
        {
            Dictionary<RegisteredArgs, string> registeredArgs = 
                new Dictionary<RegisteredArgs, string>
            {
                {RegisteredArgs.XmlCommandsDump, "xml-dump"},
                {RegisteredArgs.Print, "print"},
                {RegisteredArgs.ReadXmlData, "read-xml"},
                {RegisteredArgs.Reflection, "reflection"},
                {RegisteredArgs.Json, "json"},
                {RegisteredArgs.Game, "game"},
                {RegisteredArgs.Crypto, "crypto"},
                
            };

            var hasRegisteredArg = HasValidArgument(args, registeredArgs);

            if (!hasRegisteredArg)
            {
                ProgramHelp();
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

            if (args[0] == registeredArgs[RegisteredArgs.Print])
            {
                var printTests = new ConsolePrintTool();
                printTests.PrintFirstItem();
                Console.WriteLine("");
                printTests.PrintResultsWithEfPlatform();
            }

            if (args[0] == registeredArgs[RegisteredArgs.ReadXmlData])
            {
                var xmlTests = new XmlTests();
                xmlTests.ReadXmlWithEfPlatform();
                xmlTests.ReadXmlDataAnonymousObj();
            }

            if (args[0] == registeredArgs[RegisteredArgs.Reflection])
            {
                var reflection = new ReflectionTests();
                reflection.ReadCommandProperties();
            }
            
            if (args[0] == registeredArgs[RegisteredArgs.Json])
            {
                var jsonTool = new JsonDumpTool();
                jsonTool.PrintCommandsJson();
                jsonTool.PrintCommandsJsonWhere(c => c.Platform == "Entity Framework CLI");
            }
            if (args[0] == registeredArgs[RegisteredArgs.Game])
            {
                var other = new LearningGame();
                other.RunLearningGame();
            }
            if (args[0] == registeredArgs[RegisteredArgs.Crypto])
            {
                var hash = new CryptoTest();
                hash.EncryptPass();
                
            }
            
            
            
 
        }

        private static void GenerateXmlDump(string path = "")
        {
            var generator = new XmlDumpTool();
            generator.DumpCommands(path);
            generator.DumpCommandsWithEfPlatform(path);
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