using System;
using System.Linq;
using System.Xml.Linq;

namespace CommandsAndSnippetsTools.DumpTools
{
    public class XmlTests
    {
        public void ReadXmlWithEfPlatform()
        {
            var projectPath = Environment.CurrentDirectory;
            var testFilePath = $@"{projectPath}/XmlDumps/CommandsDumpedLinq.xml";
            XDocument customers = XDocument.Load(@testFilePath);
            
            System.Console.WriteLine($"Trying reading {testFilePath} file with EF platform...");
            
            var xml = from x in customers.Descendants("Command")
                where x.Attribute("Platform").Value == "Entity Framework CLI"
                select x;

            foreach (var x in xml)
            {
                Console.WriteLine(x);
            }
        }
        public void ReadXmlDataAnonymousObj()
        {
            var projectPath = Environment.CurrentDirectory;
            var testFilePath = $@"{projectPath}/XmlDumps/CommandsDumpedLinq.xml";
            XDocument commands = XDocument.Load(@testFilePath);
            
            Console.WriteLine($"Trying reading {testFilePath} file...");

            var cmdData = from x in commands.Descendants("Command")
                where x.Attribute("Platform").Value == "Entity Framework CLI"
                select new { 
                    Platform = x.Attribute("Platform").Value,
                    CommandLine = x.Attribute("Command").Value,
                    HowTo = x.Attribute("HowTo").Value
                } ;

            foreach (var obj in cmdData)
            {
                Console.WriteLine($"Command: {obj.CommandLine}, " +
                                  $"Platform: {obj.Platform}, " +
                                  $"How To: {obj.HowTo}");
            }

            
        }
        
    }
}