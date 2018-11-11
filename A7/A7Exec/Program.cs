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
            long[] ss = new long[] { 1, 2, 2, 2, 1, 3, 3, 3, 2, 2, 3, 2, 1, 2, 3, 3, 2, 2, };
            //Console.WriteLine(ss.Sum());
            ss = ss.OrderByDescending(x => x).ToArray();
            A7.PartitioningSouvenirs obj = new A7.PartitioningSouvenirs("shit");
            Console.WriteLine(obj.Solve(ss.Length,ss));
            Console.Read();
        }
        
        
    }
}
