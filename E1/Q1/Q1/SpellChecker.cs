using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SpellChecker
    {
        public readonly FastLM LanguageModel;

        public SpellChecker(FastLM lm)
        {
            this.LanguageModel = lm;
        }

        public string[] Check(string misspelling)
        {
            List<WordCount> candidates = 
                new List<WordCount>();

            var data = LanguageModel.WordCounts;
            var checkingWords = CandidateGenerator.GetCandidates(misspelling);

            for (int i = 0; i < checkingWords.Length - 1; i++)
            {
                var temp = BinarySearch(checkingWords[i], data);
                if (temp.Count != 0)
                {
                    candidates.Add(temp);
                }

            }

            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public string[] SlowCheck(string misspelling)
        {

            List<WordCount> candidates =
                new List<WordCount>();
            var data = LanguageModel.WordCounts;
            
            for (int i = 0; i < data.Length; i++)
            {
                int tempDistance = EditDistance(data[i].Word,misspelling);
                if (tempDistance == 1 || tempDistance == 0)
                    candidates.Add(data[i]);
            }

            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public int EditDistance(string str1, string str2)
        {
            int[,] DistanceEdit = new int[str1.Length + 1, str2.Length + 1];
            DistanceEdit[0, 0] = 0;

            for (int i = 1; i <= str1.Length; i++)
                DistanceEdit[i, 0] = i;

            for (int j = 1; j <= str2.Length; j++)
                DistanceEdit[0, j] = j;

            for (int i = 1; i < DistanceEdit.GetLength(0); i++)
                for (int j = 1; j < DistanceEdit.GetLength(1); j++)
                {
                    int insertion = DistanceEdit[i, j - 1] + 1;
                    int deletion = DistanceEdit[i - 1, j] + 1;
                    int substitution = DistanceEdit[i - 1, j - 1] + 1;
                    int match = DistanceEdit[i - 1, j - 1];
                    if (str1[i - 1] == str2[j - 1])
                        DistanceEdit[i, j] = Min(insertion, deletion, match);
                    else
                        DistanceEdit[i, j] = Min(insertion, deletion, substitution);
                }
            return DistanceEdit[str1.Length, str2.Length];
        }



        private int Min(int insertion, int deletion, int matchOrmismatch)
        {
             return Math.Min(insertion, deletion) > matchOrmismatch ? matchOrmismatch : Math.Min(insertion, deletion);
        }
        

        private WordCount BinarySearch(string word, WordCount[] wordCounts)
        {
            long low = 0, high = wordCounts.Length - 1;
            while (true)
            {
                if (low > high)
                    return new WordCount("NOT_FOUND",0);
                long mid = (low + high) / 2;
                if (string.Compare(word, wordCounts[mid].Word) == 0)
                    return wordCounts[mid];
                else if (string.Compare(word, wordCounts[mid].Word) < 0)
                    high = mid - 1;
                else if (string.Compare(word, wordCounts[mid].Word) > 0)
                    low = mid + 1;
            }
        }
    }
}
