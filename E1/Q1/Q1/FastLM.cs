using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class FastLM
    {
        public readonly WordCount[] WordCounts;


        public FastLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts.OrderBy(wc => wc.Word).ToArray();
        }

        public bool GetCount(string word, out ulong count)
        {
            count = 0;
            bool res = BinarySearch(word, WordCounts, ref count); 
            return res;
        }

        private bool BinarySearch(string word, WordCount[] wordCounts, ref ulong count)
        {
            long low = 0, high = wordCounts.Length - 1;
            while (true)
            {
                if (low > high)
                    return false;

                long mid = (low + high) / 2;

                if (string.Compare(word, wordCounts[mid].Word) == 0)
                {
                    count = wordCounts[mid].Count;
                    return true;
                }
                else if (string.Compare(word, wordCounts[mid].Word) < 0)
                    high = mid - 1;
                else if (string.Compare(word, wordCounts[mid].Word) > 0)
                    low = mid + 1;
            }
        }
    }
}
