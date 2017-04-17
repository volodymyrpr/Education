using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._14._Concurrency_and_Asynchrony
{
    public class TaskBasedAsynchronousPattern
    {
        private async Task<int> Delay1()
        {
            await Task.Delay(1000);
            return 1;
        }

        private async Task<int> Delay2()
        {
            await Task.Delay(2000);
            return 2;
        }

        private async Task<int> Delay3()
        {
            await Task.Delay(3000);
            return 3;
        }

        private async void WhenAny()
        {
            Task<int> winningTask = await Task.WhenAny(Delay1(), Delay2(), Delay3());
            Console.WriteLine("Done");
            Console.WriteLine(winningTask.Result);
        }

        private async void WhenAll()
        {
            //int[] results = await Task.WhenAll(Delay1(), Delay2(), Delay3());
            //Console.WriteLine("Done");
            //Console.WriteLine(results[0] + ", " + results[1] + ", " + results[2]);

            //Task task1 = Task.Run(async () => { await Task.Delay(1000); throw null; });
            //Task task2 = Task.Run(async () => { await Task.Delay(1000); throw null; });
            //Task combined = Task.WhenAll(task1, task2);

            //try
            //{
            //    await combined;
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine(combined.Exception.InnerExceptions.Count);
            //}

            //Task<int> task1 = Task.Run(() => 1);
            //Task<int> task2 = Task.Run(() => 2);

            //int[] results = await Task.WhenAll(task1, task2);

            //foreach(var res in results)
            //{
            //    Console.WriteLine(res);
            //}

            var size = await GetTotalSize(new[] { "http://football.ua", "http://facebook.com" });

            Console.WriteLine(size);
        }

        private async Task<int> GetTotalSize(string[] uris)
        {
            IEnumerable<Task<int>> downloadTasks = uris.Select(async uri =>
                (await new WebClient().DownloadDataTaskAsync(uri)).Length);

            int[] contents = await Task.WhenAll(downloadTasks);
            return contents.Sum();
        }

        public void Execute()
        {
            //WhenAny();
            WhenAll();
        }
    }
}
