using System;
using System.Linq;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            Graph maze = new Graph(nodeCount, edges);
            return maze.FindPath((int)StartNode, (int)EndNode) ? 1 : 0;
        }    
     }
}
