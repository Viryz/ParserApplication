using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserApplication
{
    public class UnParameterClass : IWrite
    {
        public UnParameterClass()
        {
            Console.WriteLine("Ctor");
        }

        public void Execute()
        {
            Console.WriteLine("Execute");
        }
    }
}
