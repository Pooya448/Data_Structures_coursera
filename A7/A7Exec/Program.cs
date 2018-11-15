using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] ss = new long[] {};
            //Console.WriteLine(ss.Sum());
            ss = ss.OrderByDescending(x => x).ToArray();
            A7.PartitioningSouvenirs obj = new A7.PartitioningSouvenirs("shit");
            Console.WriteLine(obj.Solve(ss.Length,ss));
            Console.Read();
        }
        
        
    }
}
