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
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c ipconfig /all",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process p = Process.Start(psi);
            string result = p.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
        }
    }
}