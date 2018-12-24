using System;
using System.Collections;
using System.Linq;

namespace E2
{
    public class Q3BloomFilter
    {
        BitArray Filter;
        int HashFunctionCount;
        int[] chosenXs;
        public const long BigPrimeNumber = 1000000007;

        public Q3BloomFilter(int filterSize, int hashFnCount)
        {
            HashFunctionCount = hashFnCount;
            Random rnd = new Random();
            Filter = new BitArray(filterSize);
            chosenXs = new int[hashFnCount];
            for (int i = 0; i < chosenXs.Length; i++)
                chosenXs[i] = rnd.Next();
            return;
        }

        public int MyHashFunction(string str, int num)
        {
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash += str[i] * Power(num, i) % BigPrimeNumber;
                hash = hash % BigPrimeNumber;
            }
            return (int)(hash % Filter.Length);
        }

        public void Add(string str)
        {
            for (int i = 0; i < HashFunctionCount; i++)
                Filter[MyHashFunction(str,chosenXs[i])] = true;
            return;
        }

        public bool Test(string str)
        {
            int[] hashFunctionsResult = new int[HashFunctionCount];
            for (int i = 0; i < hashFunctionsResult.Length; i++)
                hashFunctionsResult[i] = MyHashFunction(str,chosenXs[i]);
            foreach (var item in hashFunctionsResult)
                if (!Filter[item])
                    return false;
            return true;
        }

        public static long Power(long a, long b, long p = BigPrimeNumber)
        {
            long result = 1;
            for (int i = 0; i < b; i++)
            {
                result *= a;
                result = result % p;
            }
            return result;
        }
    }
}