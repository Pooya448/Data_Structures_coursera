using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A11;

namespace A11
{
    class Program
    {
        static void Main(string[] args)
        {
            SplayTree ss = new SplayTree();
            ss.Find(1);
            ss.Insert(1);
            ss.Find(1);
            ss.Insert(2);
            
            ss.Insert(1000000000);
            ss.Find(1000000000);
            ss.Delete(1000000000);
            ss.Find(1000000000);
            
            Console.WriteLine();
            Console.Read();
        }
    }
}
