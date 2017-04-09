using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education._14._Concurrency_and_Asynchrony
{
    public class Cancellation
    {
        async Task Foo(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public async void Execute()
        {
            var cancelSource = new CancellationTokenSource(5000);

            try
            {
                await Foo(cancelSource.Token);
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine("Cancelled!");
            }
        }
    }
}
