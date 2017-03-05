using System;

namespace Education
{
    class Program
    {
        private IExecutable currentClass = new _12._Disposal_and_Garbage_Collection.Main();

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