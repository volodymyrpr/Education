using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Numerics;

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
            Nut nut = Nut.Hazelnut;
            Size size = Size.Large;

            List<Enum> enumElements = new List<Enum>() { nut, size };

            foreach (var enumElement in enumElements)
            {
                Console.WriteLine(enumElement.GetType().Name + " " + enumElement.ToString());
            }
        }

        enum Nut { Walnut, Hazelnut, Macadamia }
        enum Size { Small, Medium, Large }
    }
}