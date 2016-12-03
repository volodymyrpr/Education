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
            BigInteger googol1 = BigInteger.Pow(10, 100);
            BigInteger googol2 = BigInteger.Parse("1".PadRight(101, '0'));

            Console.WriteLine(googol1 == googol2);
            Console.WriteLine(googol2);

            double doubleGoogol = (double)googol1;
            var converterGoogol = (BigInteger)doubleGoogol;
            Console.WriteLine(converterGoogol);
        }
    }
}