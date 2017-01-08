using System.Data.Linq;

namespace Education.Linq
{
    class L2SContext : DataContext
    {
        public L2SContext() : base("Data Source=WILDCREATURE;Initial Catalog=Education;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public Table<Customer> Customers => GetTable<Customer>();
    }
}
