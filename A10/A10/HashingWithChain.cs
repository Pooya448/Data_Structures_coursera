using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A10
{
    public class HashingWithChain : Processor
    {
        public HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        List<string>[] HashTable;

        public string[] Solve(long bucketCount, string[] commands)
        {
            HashTable = new List<string>[bucketCount];
            for (int i = 0; i < HashTable.Length; i++)
            {
                HashTable[i] = new List<string>();
            }
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];

                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, int start, int count, long m = BigPrimeNumber,
            long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;
            for (int i = start; i < start + count; i++)
            {
                hash += str[i] * Power(x, i - start) % p;
                hash = hash % p;
            }
            return hash % m;
        }
        public static long Power (long a, long b, long p = BigPrimeNumber)
        {
            long result = 1;
            for (int i = 0; i < b; i++)
            {
                result *= a;
                result = result % p;
            }
            return result;
        }
        public void Add(string str)
        {
            var hash = PolyHash(str, 0, str.Length, HashTable.Length);
            foreach (var item in HashTable[hash])
                if (item == str)
                    return;
            HashTable[hash].Add(str);
        }

        public string Find(string str)
        {
            var hash = PolyHash(str, 0, str.Length, HashTable.Length);
            foreach (var item in HashTable[hash])
                if (item == str)
                    return "yes";
            return "no";
        }

        public void Delete(string str)
        {
            var hash = PolyHash(str, 0, str.Length, HashTable.Length);
            for (int i = 0; i < HashTable[hash].Count; i++)
                if (HashTable[hash][i] == str)
                    HashTable[hash].RemoveAt(i);
            return;
        }

        public string Check(int i)
        {
            string str = "-";
            HashTable[i].Reverse();
            if (HashTable[i].Any())
                str = String.Join(" ", HashTable[i]);
            HashTable[i].Reverse();
            return str;
        }
    }
}
