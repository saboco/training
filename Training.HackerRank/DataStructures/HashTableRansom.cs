using System.Collections.Generic;
using Training.Common;

namespace Training.HackerRank.DataStructures
{
    public class HashTableRansom
    {
        private static string Solve(string[] magazine, string[] ransom)
        {
            return EnoughWords(magazine.ToAggregatedDictionary(), ransom.ToAggregatedDictionary())
                ? "Yes"
                : "No";
        }

        private static bool EnoughWords(IDictionary<string, int> availableWords, Dictionary<string, int> wantedWords)
        {
            if (availableWords.Count < wantedWords.Count)
            {
                return false;
            }

            foreach (var word in wantedWords)
            {
                if (!availableWords.ContainsKey(word.Key))
                {
                    return false;
                }

                if (availableWords[word.Key] - 1 < 0)
                {
                    return false;
                }

                availableWords[word.Key]--;
            }
            return true;
        }
    }
}
