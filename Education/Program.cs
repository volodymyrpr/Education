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
            SortedDictionary<Wish, int> wishEfforts = new SortedDictionary<Wish, int>(new WishPriorityComparer());

            Wish newFlat = new Wish("new flat", 1);
            Wish newJob = new Wish("new job", 2);
            Wish girlfriend = new Wish("new pc", 0);

            wishEfforts[newFlat] = 10;
            wishEfforts[newJob] = 3;
            wishEfforts[girlfriend] = 1000;

            foreach(var wish in wishEfforts.Keys)
            {
                Console.WriteLine(wish.Name + " " + wish.Priority);
            }
        }
    }

    public class Wish
    {
        public string Name;
        public int Priority;

        public Wish(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }
    }

    public class WishPriorityComparer : IComparer<Wish>
    {
        public int Compare(Wish x, Wish y)
        {
            if (x.Priority == y.Priority)
            {
                return 0;
            }
            else
            {
                return x.Priority.CompareTo(y.Priority);
            }
        }
    }
}