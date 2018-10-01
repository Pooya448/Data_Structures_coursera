using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using A2;
using System.Diagnostics;
using System.Linq;

namespace A2Tests
{
    [TestClass]
    public class A2Tests
    {
        [TestMethod]
        public void GradedTest_Stress()
        {
            int N = 20;
            int M = 10;
            int counter = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed.TotalSeconds < 100)
            {
                Random nrand = new Random(2);
                int n = nrand.Next(2, N);
                int[] Arr = new int[n];
                for (int i = 0; i < n; i++)
                {
                    Arr[i] = nrand.Next(0, M);
                }
                Console.WriteLine(Arr);
                int resNaive = Program.NaiveMaxPairewiseProduct(Arr.ToList());
                int resFast = Program.FastMaxPairewiseProduct(Arr.ToList());
                if (resFast == resNaive)
                {
                    Console.WriteLine("OK");
                    counter++;
                }
                else
                {
                    Console.WriteLine($"Wrong, {resNaive}, {resFast}");
                    Assert.Fail();
                }
                if (counter > 5)
                {
                    break;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        [DeploymentItem("TestData2", "TestData2")]
        public void GradedTest_Correctness()
        {
            TestCommon.TestTools.RunLocalTest(Program.Process);
        }

        [TestMethod, Timeout(500)]
        [DeploymentItem("TestData2", "TestData2")]
        public void GradedTest_Performance()
        {
            TestCommon.TestTools.RunLocalTest(Program.Process);
        }
    }
}
