using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace A10
{
    public class RabinKarp : Processor
    {
        public RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);

        public long[] Solve(string pattern, string text)
        {
            List<long> occurrences = new List<long>();
            long[] preComputeHashes = PreComputeHashes(text, pattern.Length);
            long patternHash = HashingWithChain.PolyHash(pattern, 0, pattern.Length);
            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                if (preComputeHashes[i] != patternHash)
                {
                    continue;
                }
                if (pattern == text.Substring(i,pattern.Length))
                {
                    occurrences.Add(i);
                }
            }
            return occurrences.ToArray();
        }

        private bool AreEqual(string text, string pattern, int start, int count)
        {
            for (int i = start; i < start + count; i++)
            {
                if (text[i] != pattern[i - start])
                {
                    return false;
                }
            }
            return true;
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long[] PreComputeHashes(
            string T, 
            int P, 
            long p = BigPrimeNumber, 
            long x = ChosenX)
        {
            long[] preComHashes = new long[T.Length - P + 1];
            preComHashes[T.Length - P] = HashingWithChain.PolyHash(T, T.Length - P, P);
            long y = HashingWithChain.Power(x,P);
            for (int i = T.Length - P - 1; i >= 0; i--)
            {
                preComHashes[i] = (x * preComHashes[i + 1] + T[i] - y * T[i + P] % p) % p;
            }
            return preComHashes;
        }
    }
}
