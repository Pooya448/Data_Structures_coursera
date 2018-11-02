using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class EditDistance: Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {
            int[,] DistanceEdit = new int[str1.Length + 1, str2.Length + 1];
            DistanceEdit[0, 0] = 0;

            for (int i = 1; i <= str1.Length; i++)
                DistanceEdit[i, 0] = i;

            for (int j = 1; j <= str2.Length; j++)
                DistanceEdit[0, j] = j;

            for (int i = 1; i < DistanceEdit.GetLength(0); i++)
                for (int j = 1; j < DistanceEdit.GetLength(1); j++)
                {
                    int insertion = DistanceEdit[i, j - 1] + 1;
                    int deletion = DistanceEdit[i - 1, j] + 1;
                    int substitution = DistanceEdit[i - 1, j - 1] + 1;
                    int match = DistanceEdit[i - 1, j - 1];
                    if (str1[i - 1] == str2[j - 1])
                        DistanceEdit[i, j] = Min(insertion, deletion, match);
                    else
                        DistanceEdit[i, j] = Min(insertion, deletion, substitution);
                }
            return DistanceEdit[str1.Length,str2.Length];
        }

        private int Min(int insertion, int deletion, int matchOrmismatch)
        {
            return Math.Min(insertion, deletion) > matchOrmismatch ? matchOrmismatch : Math.Min(insertion, deletion);
        }
    }
}
