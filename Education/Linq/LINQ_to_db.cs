using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Education.Linq
{
    class LINQ_to_db : IExecutable
    {
        public void Execute()
        {
            DataContext dataContext = new DataContext("Data Source=WILDCREATURE;Initial Catalog=Education;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Table<Customer> customers = dataContext.GetTable<Customer>();

            var filteredCustomers = customers
                .OrderBy(customer => customer.Name.Length)
                .Select(customer => customer.Name)
                .Pair()
                .Select((n, i) => "Pair " + i.ToString() + " = " + n);

            foreach (var customer in filteredCustomers)
            {
                Console.WriteLine(customer);
            }
            Console.WriteLine();

            ObjectContext objectContext = new ObjectContext("metadata = res://Education/TestModel.csdl|res://Education/TestModel.ssdl|res://Education/TestModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source=WILDCREATURE;initial catalog=Education;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\"");
            objectContext.DefaultContainerName = "EducationEntities";
            ObjectSet<Education.Customer> entityCustomers = objectContext.CreateObjectSet<Education.Customer>();

            Console.WriteLine(entityCustomers.Count());
        }
    }

    public static class Extesions
    {
        public static IEnumerable<string> Pair(this IEnumerable<string> source)
        {
            string firstHalf = null;
            foreach (var element in source)
            {
                if (firstHalf == null)
                {
                    firstHalf = element;
                }
                else
                {
                    yield return firstHalf + ", " + element;
                    firstHalf = null;
                }
            }
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

    [EdmEntityType(NamespaceName = "Education", Name = "EntitiyCustomer")]
    public partial class EntitiyCustomer
    {
        [EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        public int Id { get; set; }

        [EdmScalarProperty(EntityKeyProperty =false, IsNullable =false)]
        public string Name { get; set; }
    }
}
