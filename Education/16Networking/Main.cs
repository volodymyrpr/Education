﻿using System;
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
            HttpClientTest test = new HttpClientTest();
            test.DoSmth();
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
