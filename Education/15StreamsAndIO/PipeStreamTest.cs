using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    class PipeStreamTest
    {
        public void NamedPipeServerStart()
        {
            Console.WriteLine("named pipe server starting...");
            using (var s = new NamedPipeServerStream("superPuperPipe"))
            {
                s.WaitForConnection();
                s.WriteByte(100);
                Console.WriteLine(s.ReadByte());
            }
        }

        public void NamedPipeClientStart()
        {
            Console.WriteLine("named pipe client starting...");
            using (var s = new NamedPipeClientStream("superPuperPipe"))
            {
                s.Connect();
                Console.WriteLine(s.ReadByte());
                s.WriteByte(200);
            }
        }

        public byte[] ReadMessage(PipeStream s)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = new byte[0x1000];

            do
            {
                ms.Write(buffer, 0, s.Read(buffer, 0, buffer.Length));
            }
            while (!s.IsMessageComplete);

            return ms.ToArray();
        }

        public void NamedPipeServerStartCool()
        {
            Console.WriteLine("named pipe server starting...");
            using (var s = new NamedPipeServerStream("superPuperPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message))
            {
                s.WaitForConnection();

                byte[] msg = Encoding.UTF8.GetBytes("Hello");
                s.Write(msg, 0, msg.Length);

                Console.WriteLine(Encoding.UTF8.GetString(ReadMessage(s)));
            }
        }

        public void NamedPipeClientStartCool()
        {
            Console.WriteLine("named pipe client starting...");
            using (var s = new NamedPipeClientStream("superPuperPipe"))
            {
                s.Connect();
                s.ReadMode = PipeTransmissionMode.Message;

                Console.WriteLine(Encoding.UTF8.GetString(ReadMessage(s)));

                byte[] msg = Encoding.UTF8.GetBytes("Hello right back!");
                s.Write(msg, 0, msg.Length);
            }
        }
    }
}
