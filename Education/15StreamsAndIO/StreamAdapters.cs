using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    public class StreamAdapters
    {
        public void StreamReaderWriterTest()
        {
            using (FileStream fs = File.Create("test.txt"))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Line1");
                    sw.WriteLine("Line2");
                    sw.WriteLine("Line3");
                }
            }

            using (FileStream fs = File.OpenRead("test.txt"))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string str = null;
                    while ((str = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(str);
                    }
                }
            }
        }

        public void StreamReaderWriterTest2()
        {
            using (StreamWriter sw = File.CreateText("test.txt"))
            {
                sw.WriteLine("Line1");
                sw.WriteLine("Line2");
                sw.WriteLine("Line3");
            }

            using (StreamReader sr = File.OpenText("test.txt"))
            {
                while (sr.Peek() > -1)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
        }

        public void CharacterEncodingExample()
        {
            using (Stream s = File.Create("test.txt"))
            {
                using (var writer = new StreamWriter(s, Encoding.Unicode))
                {
                    writer.WriteLine("butі");
                }
            }

            using (FileStream reader = File.OpenRead("test.txt"))
            {
                for(int s; (s = reader.ReadByte()) > -1;)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}