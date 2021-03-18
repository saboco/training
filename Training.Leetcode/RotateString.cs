using System.Numerics;

namespace Training.Leetcode
{
    public class RotateString
    {
        public static bool IsRotateString(string a, string b)
        {
            if (a == b)
                return true;

            var mod = 1_000_000_007;
            var p = 113;
            var pInv = (int)BigInteger.ModPow(p, mod - 2, mod);

            (long hashB, long _) = CalculateHash(b, p, mod);
            (long hashA, long power) = CalculateHash(a, p, mod);

            for (int i = 0; i < a.Length; ++i)
            {
                char x = a[i];
                hashA += (power * x) - x;
                hashA %= mod;
                hashA *= pInv;
                hashA %= mod;
                if (hashA == hashB
                    && (a.Substring(i + 1) + a.Substring(0, i + 1)).Equals(b))
                {
                    return true;
                }

            }

            return false;
        }

        private static (long hash, long power) CalculateHash(string s, int p, int mod)
        {
            long power = 1, hash = 0;
            foreach (var c in s)
            {
                hash = (hash + (power * c)) % mod;
                power = power * p % mod;
            }
            return (hash, power);
        }

        public static bool IsRotateStringKMP(string a, string b)
        {
            var n = a.Length;
            if (n != b.Length)
            {
                return false;
            }

            if (n == 0)
            {
                return true;
            }

            //Compute shift table
            var shifts = new int[n + 1];
            for (var i =0;i < shifts.Length; i++)
            {
                shifts[i]=1;
            }

            int left = -1;
            for (int right = 0; right < n; ++right)
            {
                while (left >= 0 && (b[left] != b[right]))
                {
                    left -= shifts[left];
                }

                shifts[right + 1] = right - left++;
            }

            //Find match of B in A+A
            int matchLen = 0;
            foreach (var c in  (a + a))
            {
                while (matchLen >= 0 && b[matchLen] != c)
                {
                    matchLen -= shifts[matchLen];
                }

                if (++matchLen == n)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
