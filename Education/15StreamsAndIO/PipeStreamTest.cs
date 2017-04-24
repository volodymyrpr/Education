using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void AnonymousPipeServerStart()
        {
            Console.WriteLine("anonymous server pipe starting...");

            string clientExe = @"C:\Users\Володимир\Desktop\Education.exe";

            using (var tx = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
            {
                using (var rx = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable))
                {
                    string txID = tx.GetClientHandleAsString();
                    string rxID = rx.GetClientHandleAsString();

                    var startInfo = new ProcessStartInfo(clientExe, txID + " " + rxID);
                    startInfo.UseShellExecute = false;
                    Process p = Process.Start(startInfo);

                    tx.DisposeLocalCopyOfClientHandle();
                    rx.DisposeLocalCopyOfClientHandle();

                    tx.WriteByte(100);
                    Console.WriteLine("Server received: " + rx.ReadByte());

                    p.WaitForExit();
                }
            }
        }

        public void AnonymousPipeClientStart(string[] args)
        {
            Console.WriteLine("anonymous client pipe starting...");

            string rxID = args[0];
            string txID = args[1];

            using (var rx = new AnonymousPipeClientStream(PipeDirection.In, rxID))
            {
                using (var tx = new AnonymousPipeClientStream(PipeDirection.Out, txID))
                {
                    Console.WriteLine("Server received: " + rx.ReadByte());
                    tx.WriteByte(200);
                }
            }
        }
    }
}
