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
using System.Linq;

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
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var fluentContainsA = names
                .Where(name => name.Contains("a") || name.Contains("A"))
                .OrderBy(name => name.Length)
                .Select(name => name.ToUpper());

            var queryExpressionContainsA =
                from name in names
                where name.Contains("a") || name.Contains("A")
                orderby name.Length
                select name.ToUpper();

            foreach(var name in fluentContainsA)
            {
                Console.Write(name + ", ");
            }

            Console.WriteLine();

            foreach (var name in queryExpressionContainsA)
            {
                Console.Write(name + ", ");
            }
        }
    }
}