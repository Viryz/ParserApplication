using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace ParserApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "(10, 20, \"blabla\")";
            var parsed = GrammarParser.Parameters.Parse(input);
            foreach (var item in parsed)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
