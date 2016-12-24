using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public class Animal
    {
        public string Name;
        public int Popularity;
        public Zoo Zoo { get; internal set; }

        public Animal(string name, int popularity)
        {
            Name = name;
            Popularity = popularity;
        }
    }
}
