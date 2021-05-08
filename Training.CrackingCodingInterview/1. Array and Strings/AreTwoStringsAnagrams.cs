using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class AreTwoStringsAnagrams
    {
        // Time : O(n)
        // Space : O(n)
        public static bool AreAnagrams(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            { return false; }
            if (s1.Length != s2.Length)
            { return false; }
            if (s1 == s2)
            { return true; }

            var counter = new Dictionary<char, int>();
            foreach (var c in s1) // O(n)
            {
                if (counter.ContainsKey(c))
                {
                    counter[c]++;
                }
                else
                {
                    counter.Add(c, 1);
                }
            }
            foreach (var c in s2) // O(n)
            {
                if (counter.ContainsKey(c))
                {
                    counter[c]--;
                }
                else
                {
                    return false;
                }
            }
            foreach (var kv in counter)
            {
                if (kv.Value != 0)
                { return false; }
            }
            return true;
        }

        public static bool AreAnagrams2(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            { return false; }
            if (s1.Length != s2.Length)
            { return false; }
            if (s1 == s2)
            { return true; }

            var letters = new int[256];
            foreach (var c in s1)
            {
                letters[c]++;
            }
            foreach (var c in s2)
            {
                letters[c]--;
            }
            foreach (var i in letters)
            {
                if (i != 0)
                { return false; }
            }
            return true;
        }

        public static bool AreAnagrams3(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            { return false; }
            if (s1.Length != s2.Length)
            { return false; }
            if (s1 == s2)
            { return true; }

            var letters = new int[256];
            var differentLetters = 0;
            var treatedLetters = 0;
            foreach (var c in s1)
            {
                if (letters[c] == 0)
                {
                    differentLetters++;
                }
                letters[c]++;
            }
            for(var i=0; i < s2.Length; i++)
            {
                var c = s2[i];
                if (letters[c] == 0)
                {
                    return false;
                }
                letters[c]--;
                if (letters[c] == 0)
                {
                    treatedLetters++;
                    if (treatedLetters == differentLetters)
                    {
                        return i == s2.Length - 1;
                    }
                }
            }
            return false;
        }
    }
}
