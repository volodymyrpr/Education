using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class WorkingWithHttp
    {
        public void DoSmth()
        {
            CookiesTest();
        }

        private void HeadersTestWebClient()
        {
            WebClient wc = new WebClient() { Proxy = null };

            wc.Headers.Add("CustomHeader", "JustPlaying/1.0");
            wc.DownloadString("http://www.orielly.com");

            foreach (string name in wc.ResponseHeaders.Keys)
            {
                Console.WriteLine(name + "=" + wc.ResponseHeaders[name]);
            }
        }

        private async Task HeadersTestHttpClient()
        {
            var handler = new HttpClientHandler() { Proxy = null };
            var client = new HttpClient(handler);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.facebook.com/");

            //client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VisualStudio", "2015"));
            //client.DefaultRequestHeaders.Add("CustomHeader", "2015");

            var response = await client.SendAsync(request);
            Console.WriteLine(response.Headers);

            foreach (var authenticationHeaderValue in response.Headers.WwwAuthenticate)
            {
                Console.WriteLine(authenticationHeaderValue.Parameter + " : " + authenticationHeaderValue.Scheme);
            }
        }

        private async Task QueryStringsTest()
        {
            //var wc = new WebClient() { Proxy = null };
            //wc.QueryString.Add("q", "WebClient");
            //wc.QueryString.Add("hl", "fr");
            //wc.DownloadFile("http://google.com/search", "results.htm");

            var handler = new HttpClientHandler() { Proxy = null };
            var client = new HttpClient(handler);

            string search = Uri.EscapeDataString("(WebClient OR HttpClient)");
            string language = Uri.EscapeDataString("fr");
            var requestUri = "http://www.google.com/search?q=" + search + "&hl=" + language;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var fileStream = File.Create("results.htm"))
            {
                await stream.CopyToAsync(fileStream);
            }

            Console.WriteLine(requestUri);

            Process.Start("results.htm");
        }

        private void UploadingFormDataWebClient()
        {
            WebClient wc = new WebClient() { Proxy = null };

            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("Name", "Joe Albahari");
            data.Add("Company", "O'Reilly");

            byte[] result = wc.UploadValues("http://www.albahari.com/EchoPost.aspx", "POST", data);
            Console.WriteLine(Encoding.UTF8.GetString(result));
        }

        private void UploadingFormDataWebRequest()
        {
            var request = WebRequest.Create("http://www.albahari.com/EchoPost.aspx");
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string reqString = "Name=Joe+Albahari&Company=O'Reilly";
            byte[] reqData = Encoding.UTF8.GetBytes(reqString);
            request.ContentLength = reqData.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(reqData, 0, reqData.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream))
            {
                Console.WriteLine(streamReader.ReadToEnd());
            }
        }

        private async Task UploadingFormDataHttpClient()
        {
            string uri = "http://www.albahari.com/EchoPost.aspx";
            var client = new HttpClient();

            var dictionary = new Dictionary<string, string>
            {
                {"Name", "Joe Albahari" },
                {"Company", "O'Reilly" }
            };
            var content = new FormUrlEncodedContent(dictionary);

            var response = await client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private void CookiesTest()
        {
            var cc = new CookieContainer();
            var request = (HttpWebRequest)WebRequest.Create("http://www.facebook.com");
            request.Proxy = null;
            request.CookieContainer = cc;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach(Cookie c in response.Cookies)
                {
                    Console.WriteLine(" Name:   " + c.Name);
                    Console.WriteLine(" Value:  " + c.Value);
                    Console.WriteLine(" Path:   " + c.Path);
                    Console.WriteLine(" Domain:   " + c.Domain);
                }
            }
        }
    }
}
