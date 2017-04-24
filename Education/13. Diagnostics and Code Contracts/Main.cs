//#define TESTMODE
#define GAMEMODE
#undef TESTMODE
//#define LOGGINGMODE
#define DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;

namespace Education._13._Diagnostics_and_Code_Contracts
{
    class Main : IExecutable
    {
        public void Execute()
        {
            EventCounterExample();
        }

        private void IfCondition()
        {
#if GAMEMODE && !TESTMODE
            System.Console.WriteLine("in test mode!");
#endif
        }

        private void ConditionalAttribute()
        {
            WriteSomeLog();
        }

        [Conditional("LOGGINGMODE")]
        private void WriteSomeLog()
        {
            System.Console.WriteLine("writing something to the log...");
        }

        private void DebugTest()
        {
            Debug.Write("Data");
            Debug.WriteLine(23 * 24);
            int x = 5, y = 3;
            Debug.WriteIf(x > y, "x is greater than y");

            //Debug.Fail("Something terrible has happened");

            bool isFail = true;
            Debug.Assert(!isFail, "Something terrible has happened");
        }

        private void ShowMesage(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
        }

        private void TraceListeners()
        {
            Trace.Listeners.Clear();

            Trace.Listeners.Add(new TextWriterTraceListener("trace.txt"));

            System.IO.TextWriter tw = Console.Out;
            var manualTraceListener = new TextWriterTraceListener(tw);
            manualTraceListener.TraceOutputOptions = TraceOptions.DateTime | TraceOptions.Callstack;
            Trace.Listeners.Add(manualTraceListener);


            Trace.TraceWarning("something went wrong");

            Trace.Close();
            //Trace.TraceWarning("something went wrong");
        }

        private bool AddIfNotPresent<T>(IList<T> list, T item)
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);
            Contract.Ensures(list.Contains(item));

            if (list.Contains(item))
            {
                return false;
            }

            list.Add(item);
            return true;
        }

        private string Middle(string s)
        {
            Contract.Requires(s != null && s.Length >= 2);
            Contract.Ensures(Contract.Result<string>().Length <
                Contract.OldValue(s).Length);
            s = s.Substring(1, s.Length - 2);
            return s.Trim();
        }

        private void ContractAssert(int? x)
        {
            Contract.Assert(x != null, "X mustn't be null");
        }

        private void ProcessesInfo()
        {
            foreach (var p in Process.GetProcesses())
            {
                using (p)
                {
                    Console.WriteLine(p.ProcessName);
                    Console.WriteLine(" PID:        " + p.Id);
                    Console.WriteLine(" Memory:     " + p.WorkingSet64);
                    Console.WriteLine(" Threads:    " + p.Threads.Count);
                }
            }

            var currentProcess = Process.GetCurrentProcess();
            Console.WriteLine();
            Console.WriteLine(currentProcess.ProcessName);
            Console.WriteLine(" PID:        " + currentProcess.Id);
            Console.WriteLine(" Memory:     " + currentProcess.WorkingSet64);
            Console.WriteLine(" Threads:    " + currentProcess.Threads.Count);
        }

        private void StackTraceStackFrame()
        {
            A();
        }

        private void A()
        {
            B();
        }

        private void B()
        {
            C();
        }

        private void C()
        {
            StackTrace trace = new StackTrace(true);

            Console.WriteLine("Total frames: " + trace.FrameCount);
            Console.WriteLine("Current method: " + trace.GetFrame(0).GetMethod().Name);
            Console.WriteLine("Calling method: " + trace.GetFrame(1).GetMethod().Name);
            Console.WriteLine("Entry method: " + trace.GetFrame(trace.FrameCount - 1).GetMethod().Name);

            Console.WriteLine("Call Stack:");
            foreach(var frame in trace.GetFrames())
            {
                Console.WriteLine(
                    "   File: " + frame.GetFileName() +
                    "   Line: " + frame.GetFileLineNumber() +
                    "   Col: " + frame.GetFileColumnNumber() + 
                    "   Offset: " + frame.GetILOffset()+
                    "   Method: " + frame.GetMethod().Name);
            }
        }

        private void EventLogExample()
        {
            //string sourceName = "Education";
            //if (!EventLog.SourceExists(sourceName))
            //{
            //    EventLog.CreateEventSource(sourceName, "Application");
            //}

            //EventLog.WriteEntry(sourceName, "Hello, I am Vopo!", EventLogEntryType.Information);

            EventLog log = new EventLog("Application");
            Console.WriteLine("Total entries: " + log.Entries.Count);

            EventLogEntry last = log.Entries[log.Entries.Count - 1];
            Console.WriteLine("Index:           " + last.Index);
            Console.WriteLine("Source:          " + last.Source);
            Console.WriteLine("Type:            " + last.EntryType);
            Console.WriteLine("Time:            " + last.TimeWritten);
            Console.WriteLine("Message:         " + last.Message);
        }

        private void EventCounterExample()
        {
            //var x = new XElement("counters",
            //    from PerformanceCounterCategory cat in PerformanceCounterCategory.GetCategories()
            //    where cat.CategoryName.StartsWith(".NET")
            //    let instances = cat.GetInstanceNames()
            //    select new XElement("category",
            //        new XAttribute("name", cat.CategoryName),
            //        instances.Length == 0
            //        ?
            //            from c in cat.GetCounters()
            //            select new XElement("counter",
            //                new XAttribute("name", c.CounterName))
            //        :
            //            from i in instances
            //            select new XElement("instance",
            //                new XAttribute("name", i),
            //                !cat.InstanceExists(i)
            //                ?
            //                    null
            //                :
            //                    from counter in cat.GetCounters(i)
            //                    select new XElement("counter",
            //                        new XAttribute("name", counter.CounterName))
            //            )
            //    )
            //);

            //x.Save("counters.xml");

            using (PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                Console.WriteLine(pc.NextValue());
            }

            string procName = Process.GetCurrentProcess().ProcessName;
            using (PerformanceCounter pc = new PerformanceCounter("Process", "Private Bytes", procName))
            {
                Console.WriteLine(pc.NextValue());
            }
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }

    class Test
    {
        int _x, _y;

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(_x >= 0);
            Contract.Invariant(_y >= _x);
        }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public void Test1()
        {
            _x = -2;
        }

        void Test2()
        {
            _x = -3;
        }
    }
}
