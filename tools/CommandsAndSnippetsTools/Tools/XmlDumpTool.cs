using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandsAndSnippetsAPI.Data;

namespace CommandsAndSnippetsTools.Tools
{
    public class XmlDumpTool
    {
        private readonly DBContext _dbContext;

        string projectPath =>  Environment.CurrentDirectory;
        public XmlDumpTool()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void CommandsDump(string path)
        {
            var commands = _dbContext.CommandItems;
            
            var xElements = new List<XElement>();
            
            // https://docs.microsoft.com/en-gb/ef/core/querying/how-query-works
            
            foreach (var cmd in commands)
            {
                xElements.Add(new XElement("Command",
                    new XAttribute(nameof(cmd.Id), cmd.Id),
                    new XAttribute(nameof(cmd.Platform), cmd.Platform),
                    new XAttribute(nameof(cmd.HowTo), cmd.HowTo),
                    new XAttribute(nameof(cmd.CommandLine), cmd.CommandLine)));
            }
            
            var commandDump = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), new XElement("Commands", xElements));

            
            if (string.IsNullOrEmpty(path))
            {
                // @"Commands.xml" Will be saved to bin/Commands.xml
                commandDump.Save($@"{projectPath}/XmlDumps/Commands.xml");
                return;
            }
            
            commandDump.Save($@"{path}/Commands.xml");
            
            System.Console.WriteLine("[+] XML Dump - Done");
        }
    }
}