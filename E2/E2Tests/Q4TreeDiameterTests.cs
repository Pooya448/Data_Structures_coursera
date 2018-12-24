using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2.Tests
{
    [TestClass()]
    public class Q4TreeDiameterTests
    {
        [TestMethod()]
        public void Q4TreeDiameterTest()
        {
            Random rnd = new Random();
            int TestSize = rnd.Next(0,1000);
            Q4TreeDiameter td = new Q4TreeDiameter(TestSize);
            Assert.AreEqual(TestSize, td.Nodes.Count());
        }
        
        [TestMethod()]
        public void TreeDiameterN2Test()
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

            int ExpectedDiameter = 5;

            Q4TreeDiameter Test = new Q4TreeDiameter(10, TestTree);

            Assert.AreEqual(ExpectedDiameter, Test.TreeDiameterN2());
        }

        [TestMethod()]
        public void TreeDiameterNTest()
        {
            Assert.Inconclusive();
        }
    }
}