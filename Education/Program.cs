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

        [Flags]
        enum BorderSide { Left = 1, Right = 2, Top = 4, Bottom = 8 }

        enum Nut { Walnut, Hazelnut, Macadamia }
        enum Size { Small, Medium, Large }

        private void DoEverything()
        {
            Nut nut = Nut.Hazelnut;
            Size size = Size.Large;

            List<Enum> enumElements = new List<Enum>() { nut, size };

            foreach (var enumElement in enumElements)
            {
                Console.WriteLine(enumElement.GetType().Name + " " + enumElement.ToString());
            }

            DoSomethingElse();
        }

        private void DoSomethingElse()
        {
            int intSide = (int)BorderSide.Top;
            BorderSide side = (BorderSide)intSide;

            Console.WriteLine(GetIntegralValue(side));
        }

        private int GetIntegralValue(Enum anyEnum)
        {
            return (int)(object)anyEnum;
        }
    }
}