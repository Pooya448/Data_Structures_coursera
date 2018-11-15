using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class EditDistance : Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {
            int[,] distanceEdit = new int[str1.Length + 1, str2.Length + 1];
            distanceEdit[0, 0] = 0;

            for (int i = 1; i <= str1.Length; i++)
                distanceEdit[i, 0] = i;

            for (int j = 1; j <= str2.Length; j++)
                distanceEdit[0, j] = j;

            for (int i = 1; i < distanceEdit.GetLength(0); i++)
                for (int j = 1; j < distanceEdit.GetLength(1); j++)
                {
                    int insertion = distanceEdit[i, j - 1] + 1;
                    int deletion = distanceEdit[i - 1, j] + 1;
                    int substitution = distanceEdit[i - 1, j - 1] + 1;
                    int match = distanceEdit[i - 1, j - 1];
                    if (str1[i - 1] == str2[j - 1])
                        distanceEdit[i, j] = new[] { insertion, deletion, match }.Min();
                    else
                        distanceEdit[i, j] = new[] { insertion, deletion, substitution }.Min();
                }
            return distanceEdit[str1.Length, str2.Length];
        }
    }
}
