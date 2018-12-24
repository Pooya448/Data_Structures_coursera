using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

namespace A11
{
    
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);


        public long[][] Solve(long[][] nodes)
        {
            Tree tree = new Tree(nodes);
            var result = new long[3][];
            result[0] = tree.InOrder();
            result[1] = tree.PreOrder();
            result[2] = tree.PostOrder();
            return result;
        }
    }
}
