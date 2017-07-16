using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class HttpServerTest
    {
        public void DoSmth()
        {
            WebServerTest();
        }

        private void HttpListenerTest()
        {
            ListenAsync();
            WebClient wc = new WebClient();
            Console.WriteLine(wc.DownloadString("http://localhost:51111/MyApp/Request.txt"));
        }

        private void WebServerTest()
        {
            var server = new TestWebServer("http://localhost:51111/MyApp/", @"E:\Programming\TestFiles\");
            try
            {
                server.Start();
                Console.WriteLine("Server running... press Enter to stop");
                WebServerRequestTest();
                Console.ReadLine();
            }
            finally
            {
                server.Stop();
            }
        }

        private void WebServerRequestTest()
        {
            WebClient wc = new WebClient();
            Console.WriteLine(wc.DownloadString("http://localhost:51111/MyApp/TestDocument.txt"));
        }

        async static void ListenAsync()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:51111/MyApp/");
            listener.Start();

            HttpListenerContext context = await listener.GetContextAsync();

            string msg = "You asked for: " + context.Request.RawUrl;
            context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            using (Stream s = context.Response.OutputStream)
            using (StreamWriter writer = new StreamWriter(s))
            {
                await writer.WriteAsync(msg);
            }

            listener.Stop();
        }
    }

    class TestWebServer
    {
        HttpListener _listener;
        string _baseFolder;

        public TestWebServer(string uriPrefix, string baseFolder)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(uriPrefix);
            _baseFolder = baseFolder;
        }

        public async void Start()
        {
            _listener.Start();
            while (true)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    Task.Run(() => ProcessRequestAsync(context));
                }
                catch (HttpListenerException)
                {
                    break;
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private async void ProcessRequestAsync(HttpListenerContext context)
        {
            try
            {
                string filename = Path.GetFileName(context.Request.RawUrl);
                string path = Path.Combine(_baseFolder, filename);
                byte[] msg;

                if (!File.Exists(path))
                {
                    Console.WriteLine("Resource not found: " + path);
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    msg = Encoding.UTF8.GetBytes("Sorry, that path doesn't exist");
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    msg = File.ReadAllBytes(path);
                }
                context.Response.ContentLength64 = msg.Length;
                using (Stream s = context.Response.OutputStream)
                {
                    await s.WriteAsync(msg, 0, msg.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request error " + ex);
            }
        }
    }
}
