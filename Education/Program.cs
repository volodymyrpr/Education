using System;

namespace Education
{
    class Program
    {
        private IExecutable currentClass = new _15StreamsAndIO.Main();

        static void Main(string[] args)
        {
            Program p = new Program();

            p.DoEverything(args);

            Console.ReadLine();
        }

        private void DoEverything(string[] args)
        {
            if (args.Length > 0 && args[0] != null && args[0] != "")
            {
                currentClass.Execute(args);
            }
            else
            {
                currentClass.Execute();
            }
        }
    }
}