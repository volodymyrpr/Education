using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._14._Concurrency_and_Asynchrony
{
    public class Optimization
    {
        static Dictionary<string, Task<string>> _cache = new Dictionary<string, Task<string>>();

        private Task<string> GetWebPageAsync(string uri)
        {
            lock (_cache)
            {
                Task<string> htmlTask;
                if (_cache.TryGetValue(uri, out htmlTask))
                {
                    return htmlTask;
                }

                return _cache[uri] = new WebClient().DownloadStringTaskAsync(uri);
            }
        }

        private async void PrintWebPage(string uri)
        {
            Console.WriteLine(await GetWebPageAsync(uri));
        }

        public void Execute()
        {
            PrintWebPage("http://oreilly.com");
            PrintWebPage("http://oreilly.com");
        }
    }
}
