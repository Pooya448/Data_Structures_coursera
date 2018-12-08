using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] size = new long[] {10,0,5,0,3,3};
            long[] s = new long[] {6,5,4,3};
            long[] t = new long[] {6,6,5,4};
            A9.MergingTables ss = new A9.MergingTables("dsa");
            var res = ss.Solve(size, s, t);
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
