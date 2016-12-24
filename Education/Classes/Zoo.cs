using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public class Zoo
    {
        public AnimalCollection Animals;

        public string ZooName;

        public Zoo(string zooName)
        {
            this.ZooName = zooName;

            Animals = new AnimalCollection(this);
        }
    }
}
