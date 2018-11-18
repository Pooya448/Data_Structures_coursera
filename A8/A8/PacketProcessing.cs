using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class PacketProcessing : Processor
    {
        public PacketProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
            long[] arrivalTimes, 
            long[] processingTimes)
        {
            if (arrivalTimes.Length == 0)
            {
                return new long[] { };
            }
            Queue<(long, long, int)> processQueue = new Queue<(long, long, int)>();
            List<(long, long, int)> data = new List<(long, long, int)>();
            for (int i = 0; i < arrivalTimes.Length; i++)
                data.Add((arrivalTimes[i],processingTimes[i],i));
            long[] results = new long[data.Count];
            long nowTime = data[0].Item1;
            processQueue.Enqueue(data[0]);
            for (int i = 1; i < data.Count; i++)
            {
                var peekElement = processQueue.First();
                if (data[i].Item1 >= nowTime + peekElement.Item2)
                {
                    processQueue.Dequeue();
                    results[peekElement.Item3] = nowTime;
                    if (processQueue.Count > 0)
                        nowTime = Math.Max(nowTime + peekElement.Item2, processQueue.First().Item1);
                    else
                        nowTime = Math.Max(nowTime + peekElement.Item2, data[i].Item1);
                }
                if (bufferSize > processQueue.Count)
                    processQueue.Enqueue(data[i]);
                else
                    results[i] = -1;
            }
            while (processQueue.Count > 0)
            {
                var dequeuedElement = processQueue.Dequeue();
                results[dequeuedElement.Item3] = nowTime;
                if (processQueue.Count > 0)
                    nowTime = Math.Max(nowTime + dequeuedElement.Item2, processQueue.First().Item1);
            }
            return results;
        }
    }
}
