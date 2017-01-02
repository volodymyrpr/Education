using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Linq
{
    class Composition : IExecutable
    {
        public void Execute()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var filtered = names.Where(name => name.Contains("a"));
            var sorted = filtered.OrderBy(name => name);
            var query = sorted.Select(name => name.ToUpper());

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var newNames = names.Select(name => name.Replace("a", "")
                                                    .Replace("e", "")
                                                    .Replace("i", "")
                                                    .Replace("o", "")
                                                    .Replace("u", ""))
                                .Where(name => name.Length > 2)
                                .OrderBy(name => name);
            foreach (var newName in newNames)
            {
                Console.WriteLine(newName);
            }
            Console.WriteLine();

            var newNames2 = from name in names
                            select name.Replace("a", "")
                                       .Replace("e", "")
                                       .Replace("i", "")
                                       .Replace("o", "")
                                       .Replace("u", "");
            var newNames2Filtered = from newName in newNames2
                                    where newName.Length > 2
                                    orderby newName
                                    select newName;

            foreach (var newName in newNames2Filtered)
            {
                Console.WriteLine(newName);
            }
            Console.WriteLine();

            var newNamesInto = from name in names
                            select name.Replace("a", "")
                                       .Replace("e", "")
                                       .Replace("i", "")
                                       .Replace("o", "")
                                       .Replace("u", "")
                            into noVowel
                            where noVowel.Length > 2
                            orderby noVowel
                            select noVowel;
            foreach (var newName in newNamesInto)
            {
                Console.WriteLine(newName);
            }
            Console.WriteLine();

            var newNamesWrapping = from name in
                                        from name in names
                                        select name.Replace("a", "")
                                                    .Replace("e", "")
                                                    .Replace("i", "")
                                                    .Replace("o", "")
                                                    .Replace("u", "")
                                   where name.Length > 2
                                   orderby name
                                   select name;
            foreach(var name in newNamesWrapping)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
    }
}
