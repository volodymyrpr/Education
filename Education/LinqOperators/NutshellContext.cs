using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.LinqOperators
{
    public class NutshellContext : DataContext
    {
        public NutshellContext() : base("Data Source = WILDCREATURE; Initial Catalog = Education; Integrated Security = True; Connect Timeout = 15; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") { }

        public Table<Customer> Customers=> GetTable<Customer>();

        public Table<Purchase> Purchases => GetTable<Purchase>();
    }

    [Table]
    public class Customer
    {
        [Column(IsPrimaryKey = true)]
        public int ID;

        [Column]
        public string Name;

        [Association(OtherKey = "CustomerID")]
        public EntitySet<Purchase> Purchases = new EntitySet<Purchase>();
    }

    [Table]
    public class Purchase
    {
        [Column(IsPrimaryKey = true)]
        public int ID;

        [Column]
        public int? CustomerID;

        [Column]
        public string Description;

        [Column]
        public decimal Price;

        [Column]
        public DateTime Date;

        EntityRef<Customer> custRef;

        [Association(Storage = "custRef", ThisKey = "CustomerID", IsForeignKey = true)]
        public Customer Customer
        {
            get
            {
                return custRef.Entity;
            }

            set
            {
                custRef.Entity = value;
            }
        }
    }
}
