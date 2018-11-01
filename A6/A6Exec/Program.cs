using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            long[,] OptimumOperation = new long[2, n + 1];
            OptimumOperation[0, 0] = 0;
            for (int i = 2; i <= n; i++)
            {
                var TempResult = FindMin(OptimumOperation, i);
                OptimumOperation[0, i] = TempResult.Item1 + 1;
                OptimumOperation[1, i] = TempResult.Item2;
            }

            List<long> BacktrackList = new List<long>();
            BacktrackList.Add(n);
            Backtrack(OptimumOperation, n, ref BacktrackList);
            BacktrackList.Reverse();
            BacktrackList.ForEach(x => Console.WriteLine(x));
            Console.Read();
        }
        public static void Backtrack(long[,] Result, long n, ref List<long> backTrackList)
        {

            if (n == 1)
            {
                return;
            }
            backTrackList.Add(Result[1, n]);
            Backtrack(Result, Result[1, n], ref backTrackList);

            return;
        }
        public static (long, int) FindMin(long[,] optimumOperation, int i)
        {
            bool IsDiv3 = i % 3 == 0, IsDiv2 = i % 2 == 0;
            //div3 = 3, div 2 = 2, sub1 = 1
            List<(long, int)> SortingUtil = new List<(long, int)>();
            if (IsDiv2 && IsDiv3)
            {
                SortingUtil.Add((optimumOperation[0, i / 2], i / 2));
                SortingUtil.Add((optimumOperation[0, i / 3], i / 3));
            }
            else if (IsDiv2 && !IsDiv3)
            {
                SortingUtil.Add((optimumOperation[0, i / 2], i / 2));
            }
            else if (!IsDiv2 && IsDiv3)
            {
                SortingUtil.Add((optimumOperation[0, i / 3], i / 3));
            }
            SortingUtil.Add((optimumOperation[0, i - 1], i - 1));
            return SortingUtil.OrderBy(x => x.Item1).First();
        }

    }
}
