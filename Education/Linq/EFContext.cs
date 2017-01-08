using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Linq
{
    public class EFContext : ObjectContext
    {
        public EFContext() : base("metadata = res://Education/Linq.EDM.TestModel.csdl|res://Education/Linq.EDM.TestModel.ssdl|res://Education/Linq.EDM.TestModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source=WILDCREATURE;initial catalog=Education;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\"")
        {
            DefaultContainerName = "EducationEntities";
        }

        public ObjectSet<EDM.Customer> Customers => CreateObjectSet<EDM.Customer>();
        public ObjectSet<EDM.Group> Groups => CreateObjectSet<EDM.Group>();
    }
}
