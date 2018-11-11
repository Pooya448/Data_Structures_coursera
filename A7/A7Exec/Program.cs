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
            long w = 10;
            long[] bars = new long[] { 1,4,8};
            A7.MaximumGold obj = new A7.MaximumGold("shit");
            Console.WriteLine(obj.Solve(w,bars));
            Console.Read();
        }
    }
}
