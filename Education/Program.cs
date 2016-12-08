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
            TextWriter oldOut = Console.Out;

            using (TextWriter w = File.CreateText("e:\\output.txt"))
            {
                Console.SetOut(w);

                Console.WriteLine("test... 30%");

                Thread.Sleep(1500);

                Console.Write("test... 50%");

                Thread.Sleep(1500);

                Console.Write("test... 80%");
            }

            Console.SetOut(oldOut);

            System.Diagnostics.Process.Start("e:\\output.txt");
        }
    }
}