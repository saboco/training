using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class ConvertToPalindrome
    {
        public static int MinDeletion(string s)
        {
            return MinDeletion(s, 0, s.Length - 1);
        }

        private static int MinDeletion(string s, int i, int j)
        {
            if (i >= j)
            { return 0; }

            if (s[i] == s[j])
            {
                return MinDeletion(s, i + 1, j - 1);
            }
            else
            {
                return Math.Min(MinDeletion(s, i + 1, j), MinDeletion(s, i, j - 1)) + 1;
            }
        }

        public static int MinDeletionMemo(string s)
        {
            var cache = ArrayHelpers.NewMatrix(s.Length, s.Length, -1);
            return MinDeletionMemo(s, 0, s.Length - 1, cache);
        }

        private static int MinDeletionMemo(string s, int i, int j, int[,] cache)
        {
            if (i >= j)
            { return 0; }

            if (cache[i, j] != -1)
            {
                return cache[i, j];
            }

            if (s[i] == s[j])
            {
                return cache[i, j] = MinDeletionMemo(s, i + 1, j - 1, cache);
            }
            else
            {
                return cache[i, j] = Math.Min(MinDeletionMemo(s, i + 1, j, cache), MinDeletionMemo(s, i, j - 1, cache)) + 1;
            }
        }

        public static int MinDeletionDp(string s)
        {
            var dp = new int[s.Length, s.Length];

            for (var L = 1; L <= s.Length; L++)
            {
                for (var i = 0; i <= s.Length - L; i++)
                {
                    var j = i + L - 1;
                    if (i == j)
                    { continue; }

                    if (s[i] == s[j])
                    {
                        dp[i, j] = dp[i + 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Min(dp[i + 1, j], dp[i, j - 1]) + 1;
                    }
                }
            }

            return dp[0, s.Length - 1];
        }
    }
}
