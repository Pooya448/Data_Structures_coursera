using TestCommon;
using System;
using System.Collections.Generic;


namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            Heap minHeap = new Heap(array);
            minHeap.BuildMinHeap();
            return minHeap.Swaps.ToArray();
        }
    }
    
}