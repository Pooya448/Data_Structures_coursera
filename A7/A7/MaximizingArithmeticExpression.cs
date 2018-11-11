using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximizingArithmeticExpression : Processor
    {
        public MaximizingArithmeticExpression(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string expression)
        {
            var digits = expression
                        .ToCharArray()
                        .Where(x => expression.IndexOf(x) % 2 == 0)
                        .Select(x => long.Parse(x.ToString()))
                        .ToArray();
            var operators = expression
                           .ToCharArray()
                           .Where(x => expression.IndexOf(x) % 2 == 1)
                           .ToArray();
            int n = digits.Length;
            long[,] minTable = new long[n, n];
            long[,] maxTable = new long[n, n];
            for (int i = 0; i < n; i++)
            {
                minTable[i, i] = digits[i];
                maxTable[i, i] = digits[i];
            }
            for (int s = 0; s < n; s++)
                for (int i = 0; i < n - s - 1; i++)
                {
                    int j = i + s + 1;
                    var temp = MinAndMax(i, j,operators,minTable,maxTable);
                    minTable[i, j] = temp.Item1;
                    maxTable[i, j] = temp.Item2;
                }
            return maxTable[0, n - 1];
        }

        private (long,long) MinAndMax(int i, int j, char[] op,long[,] min, long[,] max)
        {
            long tempMin = long.MaxValue;
            long tempMax = long.MinValue;
            for (int k = i; k < j; k++)
            {
                tempMin = new[] { SwitchOp(max[i, k], op[k], max[k + 1, j]),
                                  SwitchOp(max[i, k], op[k], min[k + 1, j]),
                                  SwitchOp(min[i, k], op[k], max[k + 1, j]),
                                  SwitchOp(min[i, k], op[k], min[k + 1, j]),
                                  tempMin}.Min();
                tempMax = new[] { SwitchOp(max[i, k], op[k], max[k + 1, j]),
                                  SwitchOp(max[i, k], op[k], min[k + 1, j]),
                                  SwitchOp(min[i, k], op[k], max[k + 1, j]),
                                  SwitchOp(min[i, k], op[k], min[k + 1, j]),
                                  tempMax}.Max();

            }
            return (tempMin, tempMax);
        }

        private long SwitchOp(long v1, char op, long v2)
        {
            switch (op)
            {
                case '+':
                    return v1 + v2;
                case '*':
                    return v1 * v2;
                case '-':
                    return v1 - v2;
                default:
                    break;
            }
            return 0;
        }
    }
}
