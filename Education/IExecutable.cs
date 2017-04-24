using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education
{
    interface IExecutable
    {
        void Execute();

        void Execute(string[] args);
    }
}
