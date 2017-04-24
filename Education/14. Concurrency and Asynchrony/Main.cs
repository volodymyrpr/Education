using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Education._14._Concurrency_and_Asynchrony
{
    class ThreadTest
    {
        bool _done;
        public static bool staticDone;

        static readonly object _locker = new object();

        public void Go()
        {
            if (!_done)
            {
                _done = true;
                System.Console.WriteLine("Working on doing something");
            }
            else
            {
                System.Console.WriteLine("Sorry, already done");
            }
        }

        public static void MainThreadTest()
        {
            bool done = false;

            ThreadStart action = () =>
            {
                if (!done)
                {
                    System.Console.WriteLine("Working on doing something");
                    done = true;
                }
                else
                {
                    System.Console.WriteLine("Sorry, already done");
                }
            };

            new Thread(action).Start();
            action();
        }

        public void Go2()
        {
            lock (_locker)
            {
                if (!staticDone)
                {
                    System.Console.WriteLine("Working on doing something");
                    Thread.Sleep(10);
                    staticDone = true;
                }
                else
                {
                    System.Console.WriteLine("Sorry, already done");
                }
            }
        }
    }

    class PrimesStateMachine
    {
        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
        public Task Task
        {
            get
            {
                return tcs.Task;
            }
        }

        public void DisplayPrimeCountsFrom(int start)
        {
            var awaiter = GetPrimesCountAsync(start * 1000000 + 2, 1000000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult() +
                    " primes between " + (start * 1000000) + " and " + ((start + 1) * 1000000 - 1));

                if (++start < 10)
                {
                    DisplayPrimeCountsFrom(start);
                }
                else
                {
                    tcs.SetResult(null);
                }
            });
        }

        Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
    class Main : IExecutable
    {
        public void Execute()
        {
            TaskBasedAsynchronousPattern progressReportingTest = new TaskBasedAsynchronousPattern();
            progressReportingTest.Execute();
        }

        private void JoinExample()
        {
            Thread t = new Thread(WriteY);
            t.Name = "Some thread";
            t.Start();
            t.Join();
            System.Console.WriteLine("Thread Some thread has ended");

            for (int i = 0; i < 1000; i++)
            {
                System.Console.Write("x");
            }
        }

        private void WriteY()
        {
            for (int i = 0; i < 1000; i++)
            {
                System.Console.Write("y");
            }
        }

        private void SharedVariable()
        {
            //var tt = new ThreadTest();
            //new Thread(tt.Go).Start();
            //tt.Go();

            //ThreadTest.MainThreadTest();

            for (int i = 0; i < 10; i++)
            {
                var t = new Thread(() => new ThreadTest().Go2());
                t.Start();
                new ThreadTest().Go2();

                t.Join();
                System.Console.WriteLine();
                ThreadTest.staticDone = false;
            }
        }

        private void CapturedVariables()
        {
            for (int i = 0; i < 10; i++)
            {
                var temp = i;
                new Thread(() => System.Console.WriteLine(temp)).Start();
            }
            System.Console.WriteLine();

            string text = "text1";
            var t1 = new Thread(() => System.Console.WriteLine(text));

            text = "text2";
            var t2 = new Thread(() => System.Console.WriteLine(text));

            t1.Start();
            t2.Start();
        }

        private void ExceptionHandling()
        {
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened!");
            }
        }

        private void Go()
        {
            try
            {
                throw null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened in another thread!");
            }
        }

        private void Signaling()
        {
            var signal = new ManualResetEvent(false);

            new Thread(() =>
            {
                Console.WriteLine("wait for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("got signal!");
            }).Start();

            Thread.Sleep(2000);
            signal.Set();
        }

        private void PassingDataToAThread()
        {
            var someMessage = "Hello, it's vopo!";
            new Thread(() => Print(someMessage)).Start();

            var thread = new Thread(PrintOldStyle);
            thread.Start(someMessage);
        }

        private void Print(string message)
        {
            System.Console.WriteLine(message);
        }

        private void PrintOldStyle(object message)
        {
            var strMessage = (string)message;
            System.Console.WriteLine(strMessage);
        }

        private void ThreadPoolExample()
        {
            Task.Run(() => Console.WriteLine("hello, this is vopo!"));

            ThreadPool.QueueUserWorkItem(notUsed => Console.WriteLine("hello, this is vopo!"));
        }

        private void TasksExample()
        {
            Task task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("background task finished");
            });

            Console.WriteLine(task.IsCompleted);
            task.Wait();
            Console.WriteLine(task.IsCompleted);
            Console.WriteLine();

            Task<int> taskReturnValue = Task.Run(() =>
            {
                Console.WriteLine("Foo!!");
                return 3;
            });
            var result = taskReturnValue.Result;
            Console.WriteLine(result);
            Console.WriteLine();

            //Task<int> primeNumberTask = Task.Run(() =>
            //    Enumerable.Range(2, 3000000).Count(n =>
            //       Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            //Console.WriteLine(primeNumberTask.Result);
            //Console.WriteLine();

            Task taskNew = Task.Run(() => { throw null; });
            try
            {
                taskNew.Wait();
            }
            catch (Exception aex)
            {
                if (aex.InnerException is NullReferenceException)
                {
                    Console.WriteLine("Null!");
                }
                else
                {
                    throw;
                }
            }

        }

        private void Continuation()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            var awaiter = primeNumberTask.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                int result = awaiter.GetResult();
                Console.WriteLine(result);
            });
        }

        private void Continuation2()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 300000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            primeNumberTask.ContinueWith(antecedent =>
            {
                int result = antecedent.Result;
                Console.WriteLine(result);
            });
        }

        private void TaskCompletionSource()
        {
            var tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Thread.Sleep(3000);
                tcs.SetResult(42);
            }).Start();

            Task<int> task = tcs.Task;
            Console.WriteLine(task.Result);
            Console.WriteLine();

            Task<int> task2 = Run(() =>
            {
                Thread.Sleep(3000);
                return 42;
            });
            Console.WriteLine(task2.Result);
            Console.WriteLine();

            //for (int i = 0; i < 10000; i++)
            //{
            //    Delay(3000, () => { return 1547; });
            //}
            //Console.WriteLine();

            Task.Delay(3000).GetAwaiter().OnCompleted(() => { Console.WriteLine("very nice"); });
            Task.Delay(3000).ContinueWith(ant => { Console.WriteLine("very nice"); });
        }

        private void Delay(long delay, Func<int> operation)
        {
            var awaiter = GetAnswerToLife(delay, operation).GetAwaiter();
            awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult()));
        }

        private Task<int> GetAnswerToLife(long delay, Func<int> operation)
        {
            var tcs = new TaskCompletionSource<int>();
            var timer = new System.Timers.Timer(delay) { AutoReset = false };
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(operation()); };
            timer.Start();
            return tcs.Task;
        }

        Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }

        private void Asynchrony()
        {
            //Task.Run(() => DisplayPrimeCounts());
            //DisplayPrimeCounts();
            //DisplayPrimeCountsFrom(0);

            var awaiter = DisplayPrimeCountsAsync().GetAwaiter();
            awaiter.OnCompleted(() => Console.WriteLine("Done"));
        }

        //private void DisplayPrimeCounts()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var awaiter = GetPrimesCount(i * 1000000 + 2, 1000000).GetAwaiter();
        //        var savedI = i;
        //        awaiter.OnCompleted(() =>
        //            Console.WriteLine(awaiter.GetResult() +
        //                " primes between " + (savedI * 1000000) + " and " + ((savedI + 1) * 1000000 - 1)));
        //    }

        //    Console.WriteLine("Done!");
        //}

        //private void DisplayPrimeCountsFrom(int start)
        //{
        //    var awaiter = GetPrimesCount(start * 1000000 + 2, 1000000).GetAwaiter();
        //    awaiter.OnCompleted(() =>
        //    {
        //        Console.WriteLine(awaiter.GetResult() +
        //            " primes between " + (start * 1000000) + " and " + ((start + 1) * 1000000 - 1));

        //        if (++start < 10)
        //        {
        //            DisplayPrimeCountsFrom(start);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Done!");
        //        }
        //    });
        //}

        private Task DisplayPrimeCountsAsync()
        {
            var machine = new PrimesStateMachine();
            machine.DisplayPrimeCountsFrom(0);
            return machine.Task;
        }

        private async Task DisplayPrimeCountsAsyncCool()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await GetPrimesCountAsync(i * 1000000 + 2, 1000000) +
                    " primes between " + (i*1000000) + " and " + ((i + 1) * 1000000 - 1));
            }
        }

        Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        private async void AsynchronousFunctions()
        {
            await GoNew();
        }

        async Task GoNew()
        {
            await PrintAnswerToLife();
            Console.WriteLine();
        }

        async Task PrintAnswerToLife()
        {
            int answer = await GetSomeValue();
            Console.WriteLine(answer);
        }

        private Task PrintAnswerToLife2()
        {
            var tcs = new TaskCompletionSource<object>();
            var awaiter = Task.Delay(5000).GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                try
                {
                    awaiter.GetResult();
                    int answer = 21 * 2;
                    Console.WriteLine(answer);
                    tcs.SetResult(null);
                }
                catch(Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        async Task<int> GetSomeValue()
        {
            await Task.Delay(3000);
            return 21 * 2;
        }

        private async void LambdaExpressionsAsync()
        {
            Func<Task<string>> unnamed = async () =>
            {
                await Task.Delay(2000);
                return "Something happened again";
            };

            var task1 = NamedMethod();
            var task2 = unnamed();

            task1.ContinueWith(task => Console.WriteLine(task.Result));
            task2.ContinueWith(task => Console.WriteLine(task.Result));

            await task1;
            await task2;
        }

        private async Task<string> NamedMethod()
        {
            await Task.Delay(3000);
            return "Something happened";
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
