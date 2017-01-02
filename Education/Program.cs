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
        private IExecutable currentClass = new Linq.Projection();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.DoEverything();

            Console.ReadLine();
        }

        private void DoEverything()
        {
            currentClass.Execute();
        }
    }
}