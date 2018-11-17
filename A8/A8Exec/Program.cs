using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "[very(strong]test)";
            var temp = new A8.CheckBrackets("sd");
            temp.Solve(str);
            Console.Read();
        }
    }
}
