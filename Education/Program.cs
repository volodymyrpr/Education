using System;
using System.Collections.Generic;
using System.Text;

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
            StringBuilder sb = new StringBuilder("The string is: ");
            for (int i=0; i<50; i++)
            {
                sb.Append(i + ", ");
            }

            Console.WriteLine(sb);
        }
    }
}