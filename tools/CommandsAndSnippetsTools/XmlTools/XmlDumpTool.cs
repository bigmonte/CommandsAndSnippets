using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandsAndSnippetsAPI.Data;

namespace CommandsAndSnippetsTools.XmlTools
{
    public class XmlDumpTool
    {
        private readonly DBContext _dbContext;

        public XmlDumpTool()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void DumpCommands(string path)
        {
            var commands = _dbContext.CommandItems.ToList();
            
            var query = new XElement("Commands",
                from c in commands
                select
                    new XElement("Command",
                        new XAttribute("Id", c.Id),
                        new XAttribute("Platform", c.Platform),
                        new XAttribute("Command", c.CommandLine),
                        new XAttribute("HowTo", c.HowTo)));
            
            var commandDump = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), query);

            
            SaveXmlToFile(path, commandDump, "CommandsDumpedLinq.xml");
        }

        public void DumpCommandsWithEfPlatform(string path)
        {
            var commands = _dbContext.CommandItems.ToList();
            
            var query = new XElement("Commands",
                from c in commands
                where c.Platform == "Entity Framework CLI"
                select
                    new XElement("Command",
                        new XAttribute("Platform", c.Platform),
                        new XAttribute("Command", c.CommandLine),
                        new XAttribute("HowTo", c.HowTo)));
            
            var commandDump = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), query);

            
            SaveXmlToFile(path, commandDump, "CommandsWithEFPlatform.xml");
        }

        
        private static void SaveXmlToFile(string path, XDocument commandDump, string fileName)
        {
            if (string.IsNullOrEmpty(path))
            {
                var projectPath = Environment.CurrentDirectory;
                var defaultPath = $@"{projectPath}/XmlDumps";
                var filePath = $"{defaultPath}/{fileName}";

                // @"Commands.xml" Will be saved to bin/Commands.xml
                if (Directory.Exists(defaultPath))
                {
                    commandDump.Save(filePath);
                }
                else
                {
                    Directory.CreateDirectory(defaultPath);
                    commandDump.Save(filePath);
                }

                Console.WriteLine($"Xml dumped to: {filePath}");
                return;
            }

            // When a path was provided

            string givenPathFile = $@"{path}/Commands.xml";
            commandDump.Save(givenPathFile);
            Console.WriteLine($"Xml dumped to: {givenPathFile}");
        }
    }
}