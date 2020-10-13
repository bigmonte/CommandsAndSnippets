using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsTools.DumpTools
{
    public static class XmlExtensionMethods
    {
        public static XDocument ToXml(this List<Command> commands)
        {
            var query = new XElement("Commands",
                from c in commands
                select
                    new XElement("Command",
                        new XAttribute("Id", c.Id),
                        new XAttribute("Platform", c.Platform),
                        new XAttribute("Command", c.CommandLine),
                        new XAttribute("HowTo", c.HowTo)));
            
            var finalXml = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), query);
            
            return finalXml;
        }
        public static XDocument ToXmlWhere(this List<Command> commands, Func<Command, bool> whereCondition)
        {
            var query = new XElement("Commands",
                from c in commands
                where whereCondition(c)
                select
                    new XElement("Command",
                        new XAttribute("Id", c.Id),
                        new XAttribute("Platform", c.Platform),
                        new XAttribute("Command", c.CommandLine),
                        new XAttribute("HowTo", c.HowTo)));
            
            var finalXml = new XDocument(new XDeclaration("1.0",
                "utf-8", "yes"), query);
            
            return finalXml;
        }
        
    }
}