using System;
using System.Linq;
using System.Xml.Linq;

namespace CommandsAndSnippetsTools.XmlTools
{
    public class XmlTests
    {
        public void ReadXmlData()
        {
            var projectPath = Environment.CurrentDirectory;
            var testFilePath = $@"{projectPath}/XmlDumps/Commands.xml";
            XDocument customers = XDocument.Load(@testFilePath);

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
            var testFilePath = $@"{projectPath}/XmlDumps/Commands.xml";
            XDocument customers = XDocument.Load(@testFilePath);

            var xml = from x in customers.Descendants("Command")
                where x.Attribute("Platform").Value == "Entity Framework CLI"
                select new { 
                    Platform = x.Attribute("Platform").Value,
                    CommandLine = x.Attribute("CommandLine").Value,
                    HowTo = x.Attribute("HowTo").Value
                } ;

            foreach (var obj in xml)
            {
                Console.WriteLine($"Command: {obj.CommandLine}, " +
                                  $"Platform: {obj.Platform}, " +
                                  $"How To: {obj.HowTo}");
            }

            
        }
        
    }
}