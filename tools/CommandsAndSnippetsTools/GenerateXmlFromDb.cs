using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CommandsAndSnippetsAPI.Data;

namespace CommandsAndSnippetsTools
{
    public class GenerateXmlFromDb
    {
        private readonly DBContext _dbContext;

        public GenerateXmlFromDb()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void Generate()
        {
            // Pretty much equivalent to the for/foreach iteration in _dbContext.CommandItems
            
            var xElements = _dbContext.CommandItems
                .Select(
                    cmd => new XElement("Command", 
                        new XAttribute(nameof(cmd.Id), cmd.Id), 
                        new XAttribute(nameof(cmd.Platform), cmd.Platform), 
                        new XAttribute(nameof(cmd.HowTo), cmd.HowTo), 
                        new XAttribute(nameof(cmd.CommandLine), cmd.CommandLine))).ToList();


            var commandDump = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), new XElement("Commands", xElements));

            // Will be saved to ./bin/Commands
            commandDump.Save(@"Commands.xml");
        }
    }
}