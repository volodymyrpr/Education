using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._14._Concurrency_and_Asynchrony
{
    public class ProgressReporting
    {
        private Task Foo(Action<int> reportingAction)
        {
            return Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    reportingAction(i);
                    await Task.Delay(1000);
                }
            });
        }

        private Task Foo2(IProgress<int> onProgressPercentChanged)
        {
            return Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    onProgressPercentChanged.Report(i);
                    await Task.Delay(1000);
                }
            });
        }

        public async void Execute()
        {
            //await Foo((int i) => Console.WriteLine("this is step number {0}", i));

            var progress = new Progress<int>(i => Console.WriteLine("this is step number {0}", i));
            await Foo2(progress);
        }
    }
}
