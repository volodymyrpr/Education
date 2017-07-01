using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class AuthenticationTest
    {
        public void DoSmth()
        {
        }

        private void Test1()
        {
            var wc = new WebClient() { Proxy = null };
            wc.BaseAddress = "ftp://ftp.albahari.com";

            string username = "nutshell";
            string password = "oreilly";
            wc.Credentials = new NetworkCredential(username, password);

            wc.DownloadFile("guestbook.txt", "guestbook.txt");

            Process.Start("guestbook.txt");
        }

        private void CredentialCacheTest()
        {
            CredentialCache cache = new CredentialCache();
            Uri prefix = new Uri("http://exchange.somedomain.com");

        }
    }
}
