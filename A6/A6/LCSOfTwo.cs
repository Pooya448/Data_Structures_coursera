using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class LCSOfTwo: Processor
    {
        public LCSOfTwo(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2)
        {
            int[,] lcsTable = new int[seq1.Length + 1, seq2.Length + 1];

            for (int i = 1; i <= seq1.Length; i++)
                lcsTable[i, 0] = 0;

            for (int j = 1; j <= seq2.Length; j++)
                lcsTable[0, j] = 0;

            for (int i = 1; i < lcsTable.GetLength(0); i++)
                for (int j = 1; j < lcsTable.GetLength(1); j++)
                {
                    if (seq1[i - 1] == seq2[j - 1])
                        lcsTable[i, j] = 1 + lcsTable[i - 1, j - 1];
                    else
                        lcsTable[i, j] = Math.Max(lcsTable[i, j - 1], lcsTable[i - 1, j]);
                }
            return lcsTable[seq1.Length,seq2.Length];
        }
    }
}
