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
            Console.Read();
            long[] ss = Enumerable.Range(1,17).Select(x => long.Parse(x.ToString())).ToArray();
            Enumerable.Range(1, 17).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\n\n\n\n/////////////////////////////");
            A7.PartitioningSouvenirs obj = new A7.PartitioningSouvenirs("shit");
            Console.WriteLine(obj.Solve(ss.Length,ss));
            Console.Read();
        }
        
        
    }
}
