using Microsoft.VisualStudio.TestTools.UnitTesting;
using A1;
using TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A1.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void AddTest()
        {
            int testNum1 = 5;
            int testNum2 = 8;
            Assert.AreEqual(13, Program.Add(testNum1, testNum2));
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A1_TestData")]
        public void GradedTest()
        {
            TestTools.RunLocalTest("A1",Program.Process);
        }
    }
}