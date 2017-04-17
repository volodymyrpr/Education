using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    class MemoryStreamTest
    {
        public void DoSomething()
        {
            using (FileStream stream = new FileStream("text.txt", FileMode.Open))
            {
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);

                foreach(var element in ms.ToArray())
                {
                    Console.WriteLine(element);
                }
            }
        }
    }
}
