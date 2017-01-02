using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Education.Linq
{
    class LINQ_to_db : IExecutable
    {
        public void Execute()
        {
            DataContext dataContext = new DataContext("Data Source=WILDCREATURE;Initial Catalog=Education;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Table<Customer> customers = dataContext.GetTable<Customer>();

            var filteredCustomers = customers
                .Where(customer => customer.Name.Contains("a"))
                .OrderBy(customer => customer.Name.Length)
                .Select(customer => customer.Name);

            foreach(var customer in filteredCustomers)
            {
                Console.WriteLine(customer);
            }
            Console.WriteLine();
        }
    }

    [Table]
    class Customer
    {
        [Column(IsPrimaryKey = true)]
        public int Id;

        [Column]
        public string Name;
    }
}
