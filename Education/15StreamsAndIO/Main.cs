using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._15StreamsAndIO
{
    class Main : IExecutable
    {
        public void Execute()
        {
            PipeStreamTest pipeTest = new PipeStreamTest();
            pipeTest.NamedPipeServerStartCool();
        }
    }
}
