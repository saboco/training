using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class SplitInValidWords
    {
        public static IEnumerable<string[]> SplitValidWords(string word, string[] dict)
        {
            var words = new List<string[]>();
            SplitValidWords(word, new HashSet<string>(dict), new List<string>(), words, 0);
            return words;
        }

        private static void SplitValidWords(string word, HashSet<string> dico, List<string> words, List<string[]> allWords, int k)
        {
            if (k == word.Length)
            { allWords.Add(words.ToArray()); }

            string current = "";
            for (var i = k; i < word.Length; i++)
            {
                current += word[i];
                if (dico.Contains(current))
                {
                    words.Add(current);
                    SplitValidWords(word, dico, words, allWords, i + 1);
                    words.RemoveAt(words.Count - 1);
                }
            }
        }
    }
}
