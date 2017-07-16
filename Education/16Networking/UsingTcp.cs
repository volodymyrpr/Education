using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class UsingTcp
    {
        public void DoSmth()
        {
            Pop3Test();
        }

        private void TestTcpAsync()
        {
            RunServerAsync();
            ClientStart();
        }

        private async void RunServerAsync()
        {
            var listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();

            try
            {
                while (true)
                {
                    Accept(await listener.AcceptTcpClientAsync());
                };
            }
            finally
            {
                listener.Stop();
            }
        }

        private async Task Accept(TcpClient tcpClient)
        {
            await Task.Yield();

            try
            {
                using (tcpClient)
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    byte[] data = new byte[5000];

                    int bytesRead = 0;
                    int chunkSize = 1;

                    while (bytesRead < data.Length && chunkSize > 0)
                    {
                        bytesRead += chunkSize = await stream.ReadAsync(data, bytesRead, data.Length - bytesRead);
                    }

                    Console.WriteLine(Encoding.Unicode.GetString(data));
                    Array.Reverse(data);
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TestTcp()
        {
            ServerStart();
            ClientStart();
        }

        private void ClientStart()
        {
            using (TcpClient client = new TcpClient("localhost", 51111))
            {
                using (var stream = client.GetStream())
                {
                    var writer = new BinaryWriter(stream);
                    writer.Write("Hello!");
                    writer.Flush();

                    var messageIn = new BinaryReader(stream).ReadString();
                    Console.WriteLine("Received on client: " + messageIn);
                }
            }
        }

        private async void ServerStart()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();

            bool isSomethingReceived = false;
            while (!isSomethingReceived)
            {
                using (TcpClient c = await listener.AcceptTcpClientAsync())
                using (NetworkStream stream = c.GetStream())
                {
                    var messageIn = new BinaryReader(stream).ReadString();
                    Console.WriteLine("Received on server: " + messageIn);

                    var writer = new BinaryWriter(stream);
                    writer.Write("Hello right back!");

                    isSomethingReceived = true;
                }
            }
        }

        private void Pop3Test()
        {
            using (TcpClient client = new TcpClient("pop.i.ua", 110))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine(ReadLine(stream));
                SendCommand(stream, "USER volodymyrpr");
                SendCommand(stream, "PASS lkasd;fla;sldfjk;ajdsf;akjsd");
                SendCommand(stream, "LIST");

                List<int> messageIDs = new List<int>();
                while(true)
                {
                    string line = ReadLine(stream);
                    if (line == ".")
                    {
                        break;
                    }
                    messageIDs.Add(int.Parse(line.Split(' ')[0]));
                }

                foreach(var id in messageIDs)
                {
                    SendCommand(stream, "RETR " + id);
                    string randomFile = Guid.NewGuid().ToString() + ".eml";
                    using (StreamWriter writer = File.CreateText(randomFile))
                    {
                        while(true)
                        {
                            string line = ReadLine(stream);
                            if (line == ".")
                            {
                                break;
                            }
                            else if (line == "..")
                            {
                                line = ".";
                            }

                            writer.WriteLine(line);
                        }
                    }

                    SendCommand(stream, "DELE " + id);
                }

                SendCommand(stream, "QUIT");
            }
        }

        private void SendCommand(Stream stream, string line)
        {
            byte[] data = Encoding.UTF8.GetBytes(line + "\r\n");
            stream.Write(data, 0, data.Length);
            string response = ReadLine(stream);
            if (!response.StartsWith("+OK"))
            {
                throw new Exception("POP Error: " + response);
            }
        }

        private string ReadLine(Stream s)
        {
            List<byte> lineBuffer = new List<byte>();
            while (true)
            {
                int b = s.ReadByte();
                if (b == 10 || b < 0)
                {
                    break;
                }
                if (b != 13)
                {
                    lineBuffer.Add((byte)b);
                }
            }

            return Encoding.UTF8.GetString(lineBuffer.ToArray());
        }
    }
}