using System;
using System.Linq;
using CommandsAndSnippetsAPI.Data;
using CommandsAndSnippetsAPI.Models;
using Newtonsoft.Json.Linq;

namespace CommandsAndSnippetsTools.DumpTools
{
    public class JsonDumpTool
    {
        private readonly DBContext _dbContext;

        public JsonDumpTool()
        {
            _dbContext = new DbAccess().DbContext;
        }

        public void PrintCommandsJson()
        {
            var commands = _dbContext.CommandItems.ToList();

            JArray objects = new JArray
            {
                from c in commands
                orderby c.Id
                select new JObject
                {
                    new JProperty(nameof(c.Id), c.Id),
                    new JProperty(nameof(c.HowTo), c.HowTo),
                    new JProperty(nameof(c.CommandLine), c.CommandLine),
                    new JProperty(nameof(c.Platform), c.Platform),
                }
            };
            
            Console.WriteLine(objects.ToString());
        }
        
        public void PrintCommandsJsonWhere(Func<Command, bool> whereLambda)
        {
            var commands = _dbContext.CommandItems.ToList();

            JArray objects = new JArray
            {
                from c in commands
                orderby c.Id
                where whereLambda(c)
                select new JObject
                {
                    new JProperty(nameof(c.Id), c.Id),
                    new JProperty(nameof(c.HowTo), c.HowTo),
                    new JProperty(nameof(c.CommandLine), c.CommandLine),
                    new JProperty(nameof(c.Platform), c.Platform),
                }
            };
            
            Console.WriteLine(objects.ToString());
        }
        
        
    }
}