using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education._17Serialization
{
    class Main : IExecutable
    {
        public void Execute()
        {
            ISerializableTest test = new ISerializableTest();
            test.DoSmth();
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
