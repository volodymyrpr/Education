using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class WebRequestResponse
    {
        public async void DoSmth()
        {
            await TestAsync();
        }

        private void Test()
        {
            WebRequest req = WebRequest.Create("http://albahari.com/nutshell/code.html");
            req.Proxy = null;
            using (WebResponse res = req.GetResponse())
            using (Stream rs = res.GetResponseStream())
            using (FileStream fs = File.Create("code.html"))
                rs.CopyTo(fs);

            Process.Start("code.html");
        }

        private async Task TestAsync()
        {
            WebRequest req = WebRequest.Create("http://albahari.com/nutshell/code.html");
            req.Proxy = null;
            using (WebResponse res = await req.GetResponseAsync())
            using (Stream rs = res.GetResponseStream())
            using (FileStream fs = File.Create("code.html"))
                await rs.CopyToAsync(fs);

            Process.Start("code.html");
        }
    }
}
