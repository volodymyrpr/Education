using System;

namespace Education
{
    class Program
    {
        private IExecutable currentClass = new _13._Diagnostics_and_Code_Contracts.Main();

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