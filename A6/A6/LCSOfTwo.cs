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
            int[,] Table = new int[seq1.Length + 1, seq2.Length + 1];

            for (int i = 1; i <= seq1.Length; i++)
                Table[i, 0] = 0;

            for (int j = 1; j <= seq2.Length; j++)
                Table[0, j] = 0;

            for (int i = 1; i < Table.GetLength(0); i++)
                for (int j = 1; j < Table.GetLength(1); j++)
                {
                    if (seq1[i - 1] == seq2[j - 1])
                        Table[i, j] = 1 + Table[i - 1, j - 1];
                    else
                        Table[i, j] = Math.Max(Table[i, j - 1], Table[i - 1, j]);
                }
            return Table[seq1.Length,seq2.Length];
        }
    }
}
