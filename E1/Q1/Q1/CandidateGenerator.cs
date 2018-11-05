using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public static class CandidateGenerator
    {
        public static readonly char[] Alphabet =
            Enumerable.Range('a', 'z' - 'a' + 1)
                      .Select(c => (char)c)
                      .ToArray();

        public static string[] GetCandidates(string word)
        {
            List<string> candidates = new List<string>();

            for (int i = 0; i <= word.Length; i++)
                for (char c = 'a'; c <= 'z'; c++)
                    candidates.Add(word.Insert(i, c.ToString()));

            for (int i = 0; i < word.Length; i++)
                candidates.Add(word.Remove(i,1));

            for (int i = 0; i < word.Length; i++)
                for (char c = 'a'; c <= 'z'; c++)
                    candidates.Add(ChangeChar(word,c,i));

            return candidates.ToArray();
        }

        private static string ChangeChar(string word, char c,int i)
        {
            var temp = word.Remove(i, 1).Insert(i, c.ToString());
            return temp;
        }
    }
}
