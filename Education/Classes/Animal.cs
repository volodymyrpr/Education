using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public class Animal
    {
        public int Popularity;
        public Zoo Zoo { get; internal set; }

        private string name;
        public string Name
        {
            get { return name; }

            set
            {
                if (Zoo != null)
                {
                    Zoo.Animals.NotifyNameChange(this, value);
                    name = value;
                }
            }
        }

        public Animal(string name, int popularity)
        {
            this.Popularity = popularity;
            this.name = name;
        }
    }
}
