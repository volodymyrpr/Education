using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class HttpClientTest
    {
        public async void DoSmth()
        {
            await DelegatingHandlerTest();
        }

        private async Task Test1()
        {
            var handler = new HttpClientHandler { UseProxy = false };
            var client = new HttpClient(handler);
            var task1 = client.GetStringAsync("http://linqpad.net");
            var task2 = client.GetStringAsync("http://albahari.com");

            Console.WriteLine(await task1);
            Console.WriteLine(await task2);
        }

        private async Task GetAsyncTest()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://albahari.com/");
            //response.EnsureSuccessStatusCode();
            var error = response.IsSuccessStatusCode;
            string html = await response.Content.ReadAsStringAsync();
            Console.WriteLine(html);
            Console.WriteLine(error);

            using (var fileStream = File.Create("code.html"))
            {
                await response.Content.CopyToAsync(fileStream);
            }

            Process.Start("code.html");
        }

        private async Task SendAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://albahari.com");
            var response = await client.SendAsync(request);

            using (var fileStream = File.Create("code.html"))
            {
                await response.Content.CopyToAsync(fileStream);
            }

            Process.Start("code.html");
        }

        private async Task PostTest()
        {
            var httpClient = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www.albahari.com/EchoPost.aspx");
            request.Content = new StringContent("something terrible");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private async Task MockingTest()
        {
            var mocker = new MockHandler(request =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("You asked for " + request.RequestUri)
                });

            var httpClient = new HttpClient(mocker);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://facebook.com");
            var response = await httpClient.SendAsync(httpRequestMessage);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private async Task DelegatingHandlerTest()
        {
            var httpClientHandler = new HttpClientHandler();
            var loggingHandler = new LoggingHandler(httpClientHandler);

            var httpClient = new HttpClient(loggingHandler);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://facebook.com/");
            var response = await httpClient.SendAsync(request);
            Console.WriteLine(response.Content.ReadAsStringAsync());
        }
    }

    class MockHandler : HttpMessageHandler
    {
        Func<HttpRequestMessage, HttpResponseMessage> _responseGenerator;

        public MockHandler(Func<HttpRequestMessage, HttpResponseMessage> responseGenerator)
        {
            _responseGenerator = responseGenerator;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = _responseGenerator(request);
            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }

    class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler nextHandler)
        {
            InnerHandler = nextHandler;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Requesting: " + request.RequestUri);
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("Got response: " + response.StatusCode);
            return response;
        }
    }
}