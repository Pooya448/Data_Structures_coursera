using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A10Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(A10.HashingWithChain.PolyHash("world",0,5));
            Console.Read();
        }
    }
}
