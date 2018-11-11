using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximumGold : Processor
    {
        public MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long Weight, long[] goldBars)
        {
            long[,] GoldValue = new long[Weight + 1, goldBars.Length + 1];

            for (int i = 0; i < GoldValue.GetLength(0); i++)
                GoldValue[i, 0] = 0;

            for (int j = 0; j < GoldValue.GetLength(1); j++)
                GoldValue[0, j] = 0;

            for (int b = 1; b < GoldValue.GetLength(1); b++)
                for (int w = 1; w < GoldValue.GetLength(0); w++)
                    if (goldBars[b - 1] > w)
                        GoldValue[w, b] = GoldValue[w, b - 1];
                    else
                        GoldValue[w, b] = Math.Max((GoldValue[w - goldBars[b - 1], b - 1] + goldBars[b - 1]),
                                                   GoldValue[w, b - 1]);
            return GoldValue[Weight, goldBars.Length];
        }
    }
}
