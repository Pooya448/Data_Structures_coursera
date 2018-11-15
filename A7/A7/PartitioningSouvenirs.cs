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

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            long Sum = souvenirs.Sum();
            if (souvenirsCount < 3 || Sum % 3 != 0 || Sum / 3 < souvenirs.Max())
                return 0;
 
            long partitionSum = Sum / 3;
            bool[,,] partitionData = new bool[partitionSum + 1, partitionSum + 1,souvenirs.Length];

            for (int i = 0; i < partitionSum + 1; i++)
                for (int j = 0; j < partitionSum + 1; j++)
                    for (int k = 0; k < souvenirsCount; k++)
                    {
                        partitionData[i, 0, k] = ((i == 0) || (souvenirs[k] == i));
                        partitionData[0, j, k] = ((j == 0) || (souvenirs[k] == j));
                    }
            for (int i = 1; i < partitionSum + 1; i++)
                for (int j = 1; j < partitionSum + 1; j++)
                    for (int k = 0; k < souvenirsCount; k++)
                    {
                        bool fillable = false;
                        if(k > 0)
                        {
                            fillable = partitionData[i, j, k - 1];
                            if ((souvenirs[k] <= i && partitionData[i - souvenirs[k], j, k - 1]) || 
                                (souvenirs[k] <= j && partitionData[i, j - souvenirs[k], k - 1]))
                                fillable = true;
                        }
                        partitionData[i, j, k] = fillable;
                    }
            return partitionData[partitionSum, partitionSum, souvenirsCount - 1] ? 1 : 0;
        }
    }
}
