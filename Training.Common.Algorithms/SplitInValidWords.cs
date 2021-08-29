using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class SplitInValidWords
    {
        public static IEnumerable<string[]> SplitValidWords(string s, string[] dict)
        {
            var words = new List<string[]>();
            SplitValidWords(s, new HashSet<string>(dict), new List<string>(), words, 0);
            return words;
        }

        private static void SplitValidWords(string s, HashSet<string> dico, List<string> words, List<string[]> allWords, int k)
        {
            if (k == s.Length)
            { allWords.Add(words.ToArray()); }

            string current = "";
            for (var i = k; i < s.Length; i++)
            {
                current += s[i];
                if (dico.Contains(current))
                {
                    words.Add(current);
                    SplitValidWords(s, dico, words, allWords, i + 1);
                    words.RemoveAt(words.Count - 1);
                }
            }
        }

        public static int Count(string s, string[] dico)
        {
            return Count(s, new HashSet<string>(dico), s.Length - 1);
        }

        private static int Count(string s, HashSet<string> dico, int i)
        {
            if (s == "")
            { return 1; }

            if (i < 0)
            { return 0; }

            var count = 0;
            if (dico.Contains(s.Substring(i, s.Length - i)))
            {
                count += Count(s.Substring(0, i), dico, i) + Count(s, dico, i - 1);
            }
            else
            {
                count += Count(s, dico, i - 1);
            }

            return count;
        }

        public static (int, string[][]) CountPrint(string s, string[] dico)
        {
            var allwords = new List<string[]>();
            return (CountPrint(s, new HashSet<string>(dico), s.Length - 1, new List<string>(), allwords), allwords.ToArray());
        }

        private static int CountPrint(string s, HashSet<string> dico, int i, List<string> words, List<string[]> allwords)
        {
            if (s == "")
            {
                allwords.Add(words.ToArray());
                return 1;
            }

            if (i < 0)
            { return 0; }

            var count = 0;
            if (dico.Contains(s.Substring(i, s.Length - i)))
            {
                words.Add(s.Substring(i));
                count += CountPrint(s.Substring(0, i), dico, i, words, allwords);
                words.RemoveAt(words.Count - 1);
            }
            count += CountPrint(s, dico, i - 1, words, allwords);


            return count;
        }

        public static int CountWays(string s, string[] dico)
        {
            return CountWays(s, new HashSet<string>(dico), s.Length - 1);
        }
        private static int CountWays(string s, HashSet<string> dico, int i)
        {
            if (i == -1)
            { return 1; }

            var count = 0;
            for (var j = i; j >= 0; j--)
            {
                if (dico.Contains(s.Substring(j, i - j + 1)))
                {
                    count += CountWays(s, dico, j - 1);
                }
            }
            return count;
        }

        public static int CountWaysMemo(string s, string[] dico)
        {
            var cache = new int[s.Length];
            for (var i = 0; i < cache.Length; i++)
            {
                cache[i] = -1;
            }
            return CountWaysMemo(s, new HashSet<string>(dico), s.Length - 1, cache);
        }
        private static int CountWaysMemo(string s, HashSet<string> dico, int i, int[] cache)
        {
            if (i == -1)
            { return 1; }

            if (cache[i] != -1)
            {
                return cache[i];
            }

            var count = 0;
            for (var j = i; j >= 0; j--)
            {
                if (dico.Contains(s.Substring(j, i - j + 1)))
                {
                    count += CountWaysMemo(s, dico, j - 1, cache);
                }
            }
            return cache[i] = count;
        }

        public static int CountWaysDp(string s, string[] d)
        {
            var dico = new HashSet<string>(d);
            var N = s.Length;
            var dp = new int[N + 1];
            dp[0] = 1;

            for (var i = 1; i <= N; i++)
            {
                for (var j = i; j >= 1; j--)
                {
                    if (dico.Contains(s.Substring(j - 1, i - j)))
                    {
                        dp[i] += dp[j - 1];
                    }
                }
            }

            return dp[N];
        }
    }
}
