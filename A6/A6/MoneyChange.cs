using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public MoneyChange(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)Solve);

        public long Solve(long n)
        {
            long[] OptimumCoins = new long[n + 1];
            OptimumCoins[0] = 0;
            long Min;
            for (int coinValue = 1; coinValue <= n; coinValue++)
            {
                Min = long.MaxValue;
                for (int j = 0; j < COINS.Length; j++)
                    if (COINS[j] <= coinValue)
                        if (1 + OptimumCoins[coinValue - COINS[j]] < Min)
                            Min = 1 + OptimumCoins[coinValue - COINS[j]];
                OptimumCoins[coinValue] = Min;
            }
            return OptimumCoins[n];
        }
            
        
    }
}
