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
            //DoSomethingWithL2S();

            //DoSomethingWithEF();

            //SomeMoreTraining();
        }

        //private void SomeMoreTraining()
        //{
        //    var context = new EFContext();

        //    var query = from g in context.Groups
        //                select
        //                    from c in context.Customers
        //                    select new { g.GroupName, c.Name };

        //    foreach(var group in query)
        //    {
        //        foreach(var customer in group)
        //        {
        //            Console.WriteLine(customer.GroupName + ": " + customer.Name);
        //        }
        //    }
        //    Console.WriteLine();

        //    context.Groups.AddObject(new EDM.Group() { Id = 2, GroupName = "SuperGroup" });

        //    context.Customers.AddObject(new EDM.Customer() { Id = 11, Name = "Superman", GroupId = 2 });
        //    context.Customers.AddObject(new EDM.Customer() { Id = 12, Name = "Batman", GroupId = 2 });

        //    context.SaveChanges();
        //}

        //private void DoSomethingWithL2S()
        //{
        //    var context = new L2SContext();

        //    var filteredCustomers = context.Customers
        //        .OrderBy(customer => customer.Name.Length)
        //        .Select(customer => customer.Name)
        //        .Pair()
        //        .Select((n, i) => "Pair " + i.ToString() + " = " + n);


        //    foreach (var customer in filteredCustomers)
        //    {
        //        Console.WriteLine(customer);
        //    }
        //    Console.WriteLine();

        //    //var firstCustomer = context.Customers.OrderBy(customer => customer.Id).FirstOrDefault();
        //    //firstCustomer.Name = "Tom the programmer";

        //    //context.SubmitChanges();

        //    //Console.WriteLine(GetCustomers());
        //}

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    using (var context = new L2SContext())
        //    {
        //        return context.GetTable<Customer>().Where(customer => customer.Name.StartsWith("Tom"));
        //    }
        //}

        //private void DoSomethingWithEF()
        //{
        //    var context = new EFContext();

        //    Console.WriteLine(context.Customers.Count());

        //    //var lastCustomer = context.Customers.OrderByDescending(customer => customer.Id).FirstOrDefault();
        //    //lastCustomer.Name = "Jay the programmer";

        //    //context.SaveChanges();

        //    DoSomethingMoreWithEF();
        //}

        //private void DoSomethingMoreWithEF()
        //{
        //    var context = new EFContext();
        //    var firstGroup = context.Groups.FirstOrDefault();

        //    foreach (var customer in firstGroup.Customers)
        //    {
        //        Console.WriteLine(customer.Name);
        //    }
        //    Console.WriteLine();
        //}
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

        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
        public string Name { get; set; }
    }
}
