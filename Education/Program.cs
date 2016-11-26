using System;
using System.Collections.Generic;
using System.IO;
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
            var now = DateTime.Now;
            now = DateTime.SpecifyKind(now, DateTimeKind.Utc);
            var nowOffset = new DateTimeOffset(now);

            Console.WriteLine(nowOffset);
            Console.WriteLine(DateTimeOffset.UtcNow);
        }
    }
}