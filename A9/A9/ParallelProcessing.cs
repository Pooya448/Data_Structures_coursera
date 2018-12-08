using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    class Thread
    {
        public int Id { get; set; }
        public long JobTime { get; set; }
        public Thread(int id,long jobTime)
        {
            Id = id;
            JobTime = jobTime;
        }
    }
    public class ParallelProcessing : Processor
    {
        
        public ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);
        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            List<Thread> threads = new List<Thread>();
            Tuple<long, long>[] results = new Tuple<long, long>[jobDuration.Length];

            for (int i = 0; i < threadCount; i++)
                threads.Add(new Thread(i, 0));

            if (threads.Any())
                for (int i = 0; i < jobDuration.Length; i++)
                {
                    int j;
                    Thread tempThread = threads[0];
                    results[i] = new Tuple<long, long>(tempThread.Id, tempThread.JobTime);
                    tempThread.JobTime += jobDuration[i];
                    for (j = 0; j < threads.Count; j++)
                        if ((tempThread.JobTime == threads[j].JobTime && tempThread.Id < threads[j].Id) || 
                            tempThread.JobTime < threads[j].JobTime)
                            break;
                    threads.Insert(j, tempThread);
                    threads.RemoveAt(0);
                }
            return results;
        }
    }
}
