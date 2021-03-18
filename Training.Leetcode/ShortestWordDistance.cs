using System;

namespace Training.Leetcode
{
    public class ShortestWordDistance
    {
        public static int ShortestDistance(string[] words, string word1, string word2)
        {
            if (words.Length == 2)
            {
                return 1;
            }

            if ((words[0] == word1 && words[1] == word2)
                || (words[1] == word1 && words[0] == word2))
            {
                return 1;
            }

            var word1Index = words.Length;
            var word2Index = -1 * words.Length;
            var minDistance = int.MaxValue;
            for (var i = 0; i < words.Length; i++)
            {
                if (words[i] != word1 && words[i] != word2)
                {
                    continue;
                }

                if (words[i] == word1)
                {
                    word1Index = i;
                }
                else if (words[i] == word2)
                {
                    word2Index = i;
                }
                minDistance = Math.Min(minDistance, Math.Abs(word1Index - word2Index));
                if (minDistance == 1)
                {
                    break;
                }
            }
            return minDistance;
        }
    }
}
