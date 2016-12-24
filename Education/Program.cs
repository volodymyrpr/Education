using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Numerics;
using System.Diagnostics;
using System.Reflection;
using Education.Classes;

namespace Education
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.DoEverything();

            Console.ReadLine();
        }

        private void DoEverything()
        {
            Zoo zoo = new Zoo("Vinnytsia zoo");

            var kangaroo = new Animal("Kangaroo", 10);
            var seaLion = new Animal("Mr Sea Lion", 20);

            zoo.Animals.Add(kangaroo);
            zoo.Animals.Add(seaLion);

            foreach(var animal in zoo.Animals)
            {
                Console.WriteLine(animal.Name + " zoo: " + animal.Zoo.ZooName);
            }

            zoo.Animals.Clear();

            Console.WriteLine(kangaroo.Name + " zoo: " + kangaroo.Zoo?.ZooName);
            Console.WriteLine(seaLion.Name + " zoo: " + seaLion.Zoo?.ZooName);
        }
    }
}