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
            var order = tree.InOrder();
            var orderSort = order.OrderBy(x => x).ToArray();
            for (int i = 0, j = 1; i < order.Length && j < order.Length; i++, j++)
                if (order[i] != orderSort[i] || orderSort[i] == orderSort[j])
                    return false;
            return true;
        }
    }    
}
