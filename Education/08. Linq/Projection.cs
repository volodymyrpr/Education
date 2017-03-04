using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Linq
{
    class Projection : IExecutable
    {
        public void Execute()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var query =
                from name in names
                select new
                {
                    Original = name,
                    Vowelless = name.Replace("a", "")
                                    .Replace("e", "")
                                    .Replace("i", "")
                                    .Replace("o", "")
                                    .Replace("u", "")
                }
                into temp
                where temp.Vowelless.Length > 2
                select temp.Original;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var letQuery = from name in names
                           let vowelless = name.Replace("a", "")
                                    .Replace("e", "")
                                    .Replace("i", "")
                                    .Replace("o", "")
                                    .Replace("u", "")
                           where vowelless.Length > 2
                           select name;

            foreach(var name in letQuery)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
    }
}
