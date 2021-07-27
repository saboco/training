using System.Collections.Generic;
using System.Linq;

namespace Training.Common.Algorithms
{
    public static class Anagrams
    {
        public static IEnumerable<string> GetAnagramsIn(string word)
        {
            return Permute(word);
        }

        private static IEnumerable<string> Permute(string s)
        {
            if (s.Length == 0)
            {
                return new string[0];
            }

            if (s.Length == 1)
            {
                return new[] { s };
            }

            var permutations = new HashSet<string>();
            for (var i = 0; i < s.Length; i++)
            {
                var first = s[i];
                var rest = s.Substring(0, i) + s.Substring(i + 1);
                var newPermutations = Permute(rest);
                foreach (var p in newPermutations)
                {
                    var permutation = first + p;
                    if (!permutations.Contains(permutation))
                    {
                        permutations.Add(permutation);
                    }
                }
            }

            return permutations.ToArray();
        }

        public static IEnumerable<string> GetAnagramsBacktracking(string s)
        {
            var anagrams = new HashSet<string>();
            GetAnagramsBacktracking(s, new List<char>(), new HashSet<int>(), anagrams);
            return anagrams;
        }

        private static void GetAnagramsBacktracking(string s, List<char> current, HashSet<int> used, HashSet<string> anagrams)
        {
            if (s.Length == 0)
            { return; }
            if (current.Count == s.Length)
            { anagrams.Add(new string(current.ToArray())); }

            for (var i = 0; i < s.Length; i++)
            {
                if (!used.Contains(i))
                {
                    used.Add(i);
                    current.Add(s[i]);
                    GetAnagramsBacktracking(s, current, used, anagrams);
                    current.RemoveAt(current.Count - 1);
                    used.Remove(i);
                }
            }
        }
    }
}