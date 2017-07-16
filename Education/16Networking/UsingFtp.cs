using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class UsingFtp
    {
        public void DoSmth()
        {
            FtpCommands();
        }

        private void SimpleTest()
        {
            WebClient wc = new WebClient { Proxy = null };
            wc.Credentials = new NetworkCredential("nutshell", "oreilly");
            wc.BaseAddress = "ftp://ftp.albahari.com";
            wc.UploadString("tempfile.txt", "hello!");
            Console.WriteLine(wc.DownloadString("tempfile.txt"));
        }

        private void FtpCommands()
        {
            var req = (FtpWebRequest)WebRequest.Create("ftp://ftp.albahari.com");
            req.Proxy = null;
            req.Credentials = new NetworkCredential("nutshell", "oreilly");


            //req.Method = WebRequestMethods.Ftp.ListDirectory;

            //using (WebResponse resp = req.GetResponse())
            //{
            //    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
            //    {
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //}

            req.Method = WebRequestMethods.Ftp.GetFileSize;
            using (var response = req.GetResponse())
            {
                Console.WriteLine(response.ContentLength);
            }

            req.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            using (var res = (FtpWebResponse)req.GetResponse())
            {
                Console.WriteLine(res.LastModified);
            }

            req.Method = WebRequestMethods.Ftp.Rename;
            req.RenameTo = "deleteme.txt";
            req.GetResponse().Close();

            req.Method = WebRequestMethods.Ftp.DeleteFile;

            req.GetResponse().Close();
        }
    }
}
