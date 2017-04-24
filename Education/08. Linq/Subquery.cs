using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Linq
{
    class Subquery : IExecutable
    {
        public void Execute()
        {
            string[] musicians = { "David Gilmour", "Roger Waters", "Rick Wright", "Nick Mason" };

            var query = musicians.OrderBy(name => name.Split().Last());
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var inner = names.Min(name => name.Length);
            var withMinLength = names.Where(name => name.Length == inner);

            foreach (var name in withMinLength)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var withMinLength2 = from name in names
                                where name.Length == inner
                                select name;

            foreach (var name in withMinLength)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
