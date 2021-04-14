using System.Collections.Generic;
using System.Linq;
namespace Training.CrackingCodingInterview
{
    public class AreAllCharactersUniqueInString
    {
        // Time: O(n)
        // Space O(n)
        public static bool AreAllCharactersUnique(string s)
        {
            if (s == null)
            {
                return true;
            }

            var set = new HashSet<char>();
            foreach (var c in s)
            {
                if (!set.Add(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Time O(n^2)
        // Space O(1)
        public static bool AreAllUniqueWithoutOtherDataStructure(string s)
        {
            if (s == null)
            { return true; }
            // O(n^2)
            for (var i = 0; i < s.Length; i++) // O(n)
            {
                for (var j = i + 1; j < s.Length; j++) // O(n + (n-1) + (n-2) + ... + 1) = O(n)
                {
                    if (s[i] == s[j])
                    { return false; }
                }
            }
            return true;
        }

        public static bool AreAllUniqueSorting(string s)
        {
            if (s == null || s =="")
            { return true; }

            var sorted = s.OrderBy(c=>c).ToArray();
            var prev = sorted[0];
            for (var i = 1; i < sorted.Length; i++)
            {
                var current = sorted[i];
                if (prev == current)
                {
                    return false;
                }
                prev = current;
            }
            return true;
        }
    }
}
