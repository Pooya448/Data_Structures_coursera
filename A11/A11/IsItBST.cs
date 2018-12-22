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
            var inOrder = tree.InOrder();
            var inOrderSorted = inOrder.OrderBy(x => x).ToArray();
            for (int i = 0; i < inOrder.Length; i++)
                if (inOrder[i] != inOrderSorted[i])
                    return false;
            return true;
        }
    }    
}
