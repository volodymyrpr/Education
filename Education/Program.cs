using System;

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
            string strComposite = "Name={0,-20} Credit Limit={1,15:C}";

            Console.WriteLine(string.Format(strComposite, "Mary", 500));
            Console.WriteLine(string.Format(strComposite, "Elizabeth", 20000));
        }
    }
}