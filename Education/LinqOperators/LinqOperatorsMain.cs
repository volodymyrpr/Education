using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.LinqOperators
{
    class LinqOperatorsMain : IExecutable
    {
        string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

        NutshellContext dataContext = new NutshellContext();

        public void Execute()
        {
            WhereExecute();
        }

        private void WhereExecute()
        {
            var query = names.Where(name => name.EndsWith("y"));

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryQS = from name in names
                          where name.Length > 3
                          let nameUpper = name.ToUpper()
                          where nameUpper.EndsWith("Y")
                          select nameUpper;

            foreach (var name in queryQS)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryIndexed = names.Where((name, position) => position % 2 == 0);

            foreach(var name in queryIndexed)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryStrangeFiltering = dataContext
                .Customers
                .Where(customer => SqlMethods.Like(customer.Name, "%r%y%"))
                .Select(customer => customer.Name);

            foreach(var name in queryStrangeFiltering)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryStrangeFiltering2 = dataContext
                .Customers
                .Where(customer => customer.Name.CompareTo("J") < 0)
                .Select(customer => customer.Name);

            foreach (var name in queryStrangeFiltering2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            string[] chooseOnes = { "Tom", "Jay" };

            var queryStrangeFiltering3 = dataContext
                .Customers
                .Where(customer => chooseOnes.Contains(customer.Name))
                .Select(customer => customer.Name);

            foreach (var name in queryStrangeFiltering3)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
    }
}
