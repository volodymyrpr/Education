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
using System.Collections.ObjectModel;
using System.Collections;

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
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 1, 2, 3 };

            IStructuralEquatable a1 = a;

            Console.WriteLine(a1.Equals(b, EqualityComparer<int>.Default));

            var str1 = "Hello, my name is volodya".Split();
            var str2 = "Hello, my name is Volodya".Split();

            Console.WriteLine(((IStructuralEquatable)str1).Equals(str2, StringComparer.InvariantCultureIgnoreCase));

            var t1 = Tuple.Create(1, "foo");
            var t2 = Tuple.Create(1, "Foo");

            Console.WriteLine(((IStructuralEquatable)t1).Equals(t2, StringComparer.InvariantCultureIgnoreCase));
        }
    }
}