using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class PartitioningSouvenirs : Processor
    {
        public PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirsArr)
        {
            souvenirsArr = souvenirsArr
                           .OrderByDescending(x => x)
                           .ToArray();

            var partTuple = Partition(souvenirsCount, souvenirsArr, 3);
            if (partTuple.Item1 == null)
                return 0;

            List<long> removalList = BackTrack(partTuple.Item1, partTuple.Item2, souvenirsArr);

            List<long> souvenirs = souvenirsArr.ToList();
            removalList.ForEach(x => souvenirs.Remove(x));
            souvenirsArr = souvenirs.ToArray(); 

            partTuple = Partition(souvenirsCount, souvenirsArr, 2);
            if (partTuple.Item1 == null)
                return 0;
            else
                return partTuple.Item1[partTuple.Item2, partTuple.Item3] ? 1 : 0;
        }

        private List<long> BackTrack(bool[,] resPartition, int i, long[] souvenirs)
        {
            List<long> result = new List<long>();
            for (int j = resPartition.GetLength(1) - 1; j > 0 && i > 0; i--)
                if (resPartition[i, j] && resPartition[i - 1, j - souvenirs[i - 1]])
                {
                    result.Add(souvenirs[i - 1]);
                    j -= (int)souvenirs[i - 1];
                }
            return result;
        }

        public (bool[,],int,int) Partition(long souvenirsCount, long[] souvenirsArr, int K)
        {
            var souvenirs = souvenirsArr.ToList();
            long Sum = souvenirs.Sum();
            if (Sum % K != 0 || Sum == 0)
                return (null,-1,-1);
            long PartitionSum = Sum / K;
            bool[,] partitionTable = new bool[souvenirsCount + 1, PartitionSum + 1];
            for (int j = 0; j < partitionTable.GetLength(1); j++)
                partitionTable[0, j] = false;
            for (int i = 0; i < partitionTable.GetLength(0); i++)
                partitionTable[i, 0] = true;
            for (int i = 1; i < partitionTable.GetLength(0); i++)
            {
                for (int j = 1; j < partitionTable.GetLength(1); j++)
                    if (souvenirs[i - 1] <= j)
                        partitionTable[i, j] = partitionTable[i - 1, j] || partitionTable[i - 1, j - souvenirs[i - 1]];
                if (partitionTable[i, PartitionSum])
                    return (partitionTable,i, (int)PartitionSum);
            }
            return (null,-1,-1);
        }
    }
}
