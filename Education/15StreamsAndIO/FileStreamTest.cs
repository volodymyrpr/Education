using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    class FileStreamTest
    {
        public void DoSomething()
        {
            using (var s = new FileStream("text.txt", FileMode.Create))
            {
                Console.WriteLine(s.CanRead);
                Console.WriteLine(s.CanWrite);
                Console.WriteLine(s.CanSeek);
                Console.WriteLine(s.CanTimeout);

                s.WriteByte(101);
                s.WriteByte(102);
                byte[] block = { 1, 2, 3, 4 };
                s.Write(block, 0, block.Length);

                Console.WriteLine(s.Length);
                Console.WriteLine(s.Position);
                s.Position = 0;

                Console.WriteLine(s.ReadByte());
                Console.WriteLine(s.ReadByte());
                s.Position--;

                Console.WriteLine(s.Read(block, 0, block.Length));

                Console.WriteLine(s.Read(block, 0, block.Length));
                Console.WriteLine(s.Read(block, 0, block.Length));
            }
        }

        public async void DoSomethingAsync()
        {
            using (var s = new FileStream("test.txt", FileMode.Create))
            {
                byte[] block = { 1, 2, 3, 4, 5 };
                await s.WriteAsync(block, 1, block.Length - 1);

                s.Position = 0;

                await s.ReadAsync(block, 0, block.Length);
                WriteArray(block);
            }
        }

        public void DoSomethingElse()
        {
            //FileStream fs1 = File.OpenRead("test.txt");
            //FileStream fs3 = File.Create("writeme.tmp");
            //FileStream fs2 = File.OpenWrite("writeme.tmp");

            using (var stream = File.Open("test.txt", FileMode.Open))
            {
                stream.Seek(0, SeekOrigin.End);

                byte[] array = { 1, 2, 3 };
                stream.Write(array, 0, 3);

                Console.WriteLine(stream.Position);
            }
        }

        private void WriteArray(byte[] array)
        {
            foreach (var element in array)
            {
                Console.WriteLine(element);
            }
        }
    }
}
