using System;
using TestCommon;
using System.Linq;

namespace A11
{
    public class IsItBST : Processor
    {
        public IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);


        public bool Solve(long[][] nodes)
        {
            Tree tree = new Tree(nodes);
            return tree.IsBST();
        }
    }    
}
