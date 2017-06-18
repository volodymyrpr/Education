using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    public class BufferedStreamExample
    {
        public void DoSomething()
        {
            File.WriteAllBytes("myfile.bin", new byte[100000]);

            using (FileStream fs = File.OpenRead("myFile.bin"))
            {
                using (BufferedStream bs = new BufferedStream(fs, 20000))
                {
                    bs.ReadByte();
                    Console.WriteLine(fs.Position);
                }
            }
        }
    }
}
