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
            SortedDictionary<string, int> personsRating = new SortedDictionary<string, int>(new SurnameComparer(CultureInfo.InvariantCulture));

            personsRating["MacPetriv"] = 10;
            personsRating["MCIvaniv"] = 11;
            personsRating["MacRomaniv"] = 12;

            foreach(var person in personsRating.Keys)
            {
                Console.WriteLine(person);
            }
        }
    }

    class SurnameComparer : Comparer<string>
    {
        StringComparer strCmp;

        public SurnameComparer(CultureInfo ci)
        {
            strCmp = StringComparer.Create(ci, false);
        }

        public override int Compare(string x, string y)
        {
            return strCmp.Compare(Normalize(x), Normalize(y));
        }

        private string Normalize(string s)
        {
            s = s.Trim();
            if (s.StartsWith("MC", StringComparison.InvariantCultureIgnoreCase))
            {
                s = "MAC" + s.Substring(2);
            }

            return s;
        }
    }
}