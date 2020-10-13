using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using CommandsAndSnippetsAPI.Models;
namespace CommandsAndSnippetsTools
{
    public class ReflectionTests
    {
        public void ReadCommandProperties()
        {
            var query = from m in typeof(Command).GetProperties()
                where m.IsPublic()
                select m;
            
            foreach (var q in query)
            {
                Console.WriteLine($"{q.Name}: {q.PropertyType.Name}");
            }
        }

        
        /*
         * Retrieves the methods of the string class that are static,
         * Finds out how many overloads each method has
         * Orders them first by the number of overloads and then alphabetically:
         */
        
        
        public void StringTypeTest()
        {
            var query = from m in typeof(string).GetMethods()
                where m.IsStatic == true
                orderby m.Name
                group m by m.Name into g
                orderby g.Count()
                select new { Name = g.Key, Overloads = g.Count() };

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        public void NestedListTest()
        {
            List<int> list01 = new List<int> { 1, 2, 3 };
            List<int> list02 = new List<int> { 4, 5, 6 };
            List<int> list03 = new List<int> { 7, 8, 9 };
            
            List<List<int>> lists = new List<List<int>> { list01, list02, list03 };
            
            var newList = from list in lists
                from num in list
                select num;
            
            
            // equivalent to newList.Reverse()
            
            var query = from list in lists
                from num in list
                orderby num descending
                select num;
            
            var queryPairNumbers = from list in lists
                from num in list
                where num % 2 == 0
                orderby num descending
                select num;
        }
    }
}