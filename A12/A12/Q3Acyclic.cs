using System;
using TestCommon;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph curriculum = new Graph(nodeCount, edges, true);
            return curriculum.IsDAG() ? 0 : 1;
        }
    }
}