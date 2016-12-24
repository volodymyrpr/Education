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
            Zoo zoo = new Zoo();
            zoo.Animals.Add(new Animal("Kangaroo", 10));
            zoo.Animals.Add(new Animal("Mr Sea Lion", 20));
            foreach(var animal in zoo.Animals)
            {
                Console.WriteLine(animal.Name);
            }
        }
    }
}