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
            ListenAsync();
            WebClient wc = new WebClient();
            Console.WriteLine(wc.DownloadString("http://localhost:51111/MyApp/Request.txt"));
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
            while(true)
            {
                try
                {
                    var content = await _listener.GetContextAsync();
                    Task.Run(() => Process)
                }
            }
        }

        
    }
}
