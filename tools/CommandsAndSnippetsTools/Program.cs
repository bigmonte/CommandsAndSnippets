using System;
using System.Linq;
using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace CommandsAndSnippetsTools
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO use secrets
            
            string conString = "Server=localhost,1433\\Catalog=sql1;Database=sql1;User=SA;Password=<YourStrong@Passw0rd>;";

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            
            optionsBuilder.UseSqlServer(conString);

            using (var context = new DBContext(optionsBuilder.Options))
            {
                Console.WriteLine($"{context.CommandItems.First()?.Platform}");
            }
        }


    }
}