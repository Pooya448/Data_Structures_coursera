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
            long Sum = souvenirsArr.Sum();
            if (Sum % 3 != 0 || Sum == 0)
                return 0;
            long PartitionSum = Sum / 3;
            bool[,] resPartition = new bool[souvenirsCount + 1, PartitionSum + 1];
            var souvenirs = souvenirsArr.OrderByDescending(x => x).ToList();
            for (int j = 0; j < resPartition.GetLength(1); j++)
            {
                resPartition[0, j] = false;
            }
            for (int i = 0; i < resPartition.GetLength(0); i++)
            {
                resPartition[i, 0] = true;
            }
            bool flag = false;
            List<long> listOfRemoval = new List<long>();
            for (int i = 1; i < resPartition.GetLength(0); i++)
            {
                for (int j = 1; j < resPartition.GetLength(1); j++)
                {
                    //resPartition[i, j] = resPartition[i - 1, j];
                    if (souvenirs[i - 1] <= j)
                    {
                        resPartition[i, j] = resPartition[i - 1, j] || resPartition[i - 1, j - souvenirs[i - 1]];
                    }
                }
                if (resPartition[i, PartitionSum])
                {
                    flag = true;
                    listOfRemoval = BackTrack(resPartition, i, souvenirs.ToArray());
                    PrintArray(resPartition);
                    listOfRemoval.ForEach(x => Console.WriteLine(x));
                    Console.WriteLine("\n\n********************\n\n");
                    break;
                }
            }
            if (!flag)
            {
                return 0;
            }
            foreach (var item in listOfRemoval)
            {
                souvenirs.Remove(item);
            }
            return PartSolve(souvenirs.Count, souvenirs.ToArray()) ? 1 : 0;
        }

        private void PrintArray(bool[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.WriteLine("********************************\n******************************");
        }

        private List<long> BackTrack(bool[,] resPartition, int i, long[] souvenirs)
        {
            List<long> result = new List<long>();
            for (int j = resPartition.GetLength(1) - 1; j > 0 && i > 0; i--)
            {
                if (resPartition[i, j] && resPartition[i - 1, j - souvenirs[i - 1]])
                {
                    result.Add(souvenirs[i - 1]);
                    j -= (int)souvenirs[i - 1];
                }



            }
            return result;
        }
        public bool PartSolve(long souvenirsCount, long[] souvenirs)
        {
            long Sum = souvenirs.Sum();
            if (Sum % 2 != 0)
                return false;
            long PartitionSum = Sum / 2;
            bool[,] resPartition = new bool[souvenirsCount + 1, PartitionSum + 1];
            for (int j = 0; j < resPartition.GetLength(1); j++)
            {
                resPartition[0, j] = false;
            }
            for (int i = 0; i < resPartition.GetLength(0); i++)
            {
                resPartition[i, 0] = true;
            }
            for (int i = 1; i < resPartition.GetLength(0); i++)
            {
                for (int j = 1; j < resPartition.GetLength(1); j++)
                {
                    //resPartition[i, j] = resPartition[i - 1, j];
                    if (souvenirs[i - 1] <= j)
                    {
                        resPartition[i, j] = resPartition[i - 1, j] || resPartition[i - 1, j - souvenirs[i - 1]];
                    }
                }
            }
            PrintArray(resPartition);
            return resPartition[souvenirsCount, PartitionSum];
        }
    }
}
