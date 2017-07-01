using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class Main : IExecutable
    {
        public void Execute()
        {
            AuthenticationTest test = new AuthenticationTest();
            test.DoSmth();
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
