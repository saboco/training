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
            if (s.Length == 0) return new string[0];
            if (s.Length == 1) return new[] {s};

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
                        permutations.Add(permutation);
                }
            }

            return permutations.ToArray();
        }
    }
}