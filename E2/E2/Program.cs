using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2
{
    class Program
    {
        static void Main(string[] args)
        {
            int TestSize = 11;
            var TestTree = new List<int>[TestSize];
            TestTree[0] = new int[] { 1, 2, 3, 4 }.ToList();
            TestTree[1] = new int[] { 10 }.ToList();
            TestTree[2] = new int[] { 5 }.ToList();
            TestTree[3] = new int[] { 9 }.ToList();
            TestTree[4] = new List<int>();
            TestTree[5] = new int[] { 6, 7, 8 }.ToList();
            TestTree[6] = new List<int>();
            TestTree[7] = new List<int>();
            TestTree[8] = new List<int>();
            TestTree[9] = new List<int>();
            TestTree[10] = new List<int>();


            Q4TreeDiameter Test = new Q4TreeDiameter(10, TestTree);
            Console.WriteLine(Test.TreeDiameterN());
            Console.Read();
        }
    }
}
