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
            string str1 = "EDITING";
            string str2 = "DISTANCE";
            int[,] DistanceEdit = new int[str1.Length+1, str2.Length+1];
            DistanceEdit[0, 0] = 0;
            for (int i = 1; i <= str1.Length; i++)
                DistanceEdit[i, 0] = i;

            for (int j = 1; j <= str2.Length; j++)
                DistanceEdit[0, j] = j;

            var iCount = DistanceEdit.GetLength(0);
            var jCount = DistanceEdit.GetLength(1);

            for (int i = 1; i < DistanceEdit.GetLength(0); i++)
            {
                for (int j = 1; j < DistanceEdit.GetLength(1); j++)
                {
                    int insertion = DistanceEdit[i, j - 1] + 1;
                    int deletion = DistanceEdit[i - 1, j] + 1;
                    int substitution = DistanceEdit[i - 1, j - 1] + 1;
                    int match = DistanceEdit[i - 1, j - 1];
                    if (str1[i-1] == str2[j-1])
                        DistanceEdit[i, j] = Min(insertion, deletion, match);
                    else
                        DistanceEdit[i, j] = Min(insertion, deletion, substitution);
                }
            }
            Print2DArray(DistanceEdit);
        }
        public static int Min(int insertion, int deletion, int matchOrmismatch)
        {
            return Math.Min(insertion, deletion) > matchOrmismatch ? matchOrmismatch : Math.Min(insertion, deletion);
        }
        public static void Print2DArray (int[,] DistanceEdit)
        {
            var rowCount = DistanceEdit.GetLength(0);
            var colCount = DistanceEdit.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", DistanceEdit[row, col]));
                Console.WriteLine();
            }
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
