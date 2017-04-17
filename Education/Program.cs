using System;

namespace Education
{
    class Program
    {
        private IExecutable currentClass = new _15StreamsAndIO.Main();

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