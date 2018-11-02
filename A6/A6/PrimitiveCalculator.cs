using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class PrimitiveCalculator: Processor
    {

        public PrimitiveCalculator(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);
        public enum Op
        {
            Div3 = 3,
            Div2 = 2,
            Sub1 = 1
        }
        public static (long, int, Op) FindMin (long[,] optimumOperation,int i)
        {
            bool IsDiv3 = i % 3 == 0, IsDiv2 = i % 2 == 0;
            List<(long,int,Op)> SortHelper = new List<(long,int,Op)>();
            string Condition = IsDiv3.ToString() + ':' + IsDiv2.ToString();
            switch (Condition)
            {
                case "True:True" :
                    SortHelper.Add((optimumOperation[0, i / 2], i / 2, Op.Div2));
                    SortHelper.Add((optimumOperation[0, i / 3], i/3,Op.Div3));
                    break;
                case "True:False":
                    SortHelper.Add((optimumOperation[0, i / 3], i / 3, Op.Div3));
                    break;
                case "False:True":
                    SortHelper.Add((optimumOperation[0, i / 2], i / 2, Op.Div2));
                    break;
                default:
                    break;
            }
            SortHelper.Add((optimumOperation[0,i-1], i-1,Op.Sub1));
            return SortHelper
                   .OrderBy(x => x.Item1)
                   .ThenByDescending(x => x.Item3)
                   .First();
        }
        public long[] Solve(long n)
        { 
            long[,] OptimumOperation = new long[2,n+1];
            OptimumOperation[0,0] = 0;
            for (int i = 2; i <= n; i++)
            {
                var TempResult = FindMin(OptimumOperation, i);
                OptimumOperation[0,i] = TempResult.Item1 + 1;
                OptimumOperation[1,i] = TempResult.Item2;
            }
            return BackTrack(OptimumOperation, n);
        }
        public long[] BackTrack (long[,] Result, long n)
        {
            List<long> BacktrackList = new List<long>();
            BacktrackList.Add(n);
            Recursion(Result, n, BacktrackList);
            BacktrackList.Reverse();
            return BacktrackList.ToArray();
        }
        public void Recursion (long[,] Result,long n,List<long> backTrackList)
        {
            if (n == 1)
                return;
            backTrackList.Add(Result[1, n]);
            Recursion(Result, Result[1, n], backTrackList);
            return;
        }
    }
}
