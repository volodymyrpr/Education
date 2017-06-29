﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class WebClientTest
    {
        public void DoSmth()
        {
            WebClient();
        }

        private void UriTest()
        {
            Uri info = new Uri("http://www.domain.com:80/info/");
            Uri page = new Uri("http://www.domain.com/info/page.html");

            Console.WriteLine(info.Host);
            Console.WriteLine(info.Port);
            Console.WriteLine(page.Port);

            Console.WriteLine(info.IsBaseOf(page));
            Uri relative = info.MakeRelativeUri(page);
            Console.WriteLine(relative.IsAbsoluteUri);
            Console.WriteLine(relative.ToString());
        }

        private void WebClient()
        {
            WebClient wc = new WebClient() { Proxy = null };
            wc.DownloadFile("http://www.albahari.com/nutshell/code.aspx", "code.htm");
            System.Diagnostics.Process.Start("code.htm");
        }
    }
}
