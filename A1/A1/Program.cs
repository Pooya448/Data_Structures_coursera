using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static string Process(string input)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader reader = new StringReader(input))
            using (StringWriter writer = new StringWriter(sb))
            {
                int a = int.Parse(reader.ReadLine());
                int b = int.Parse(reader.ReadLine());
                writer.WriteLine(Add(a, b));
            }
            return sb.ToString();
        }

        public static int Add(int firstNum, int secondNum)
        {
            return firstNum + secondNum;
        }
    }
}
