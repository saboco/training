namespace Training.Codility.PrimeAndCompositeNumbers.Peaks
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            var n = a.Length;
            // the minimal divisor has the maximum number of blocks
            // 1. Find the minimal divisor d such n / d = b; where b is the number of blocks
            // 2. Find it every block has at least one peak
            // 3. If it does, return, else look for the next divisor;
            // minimal can't be less than 2

            var i = 1L;

            while (i < n)
            {
                i++;
                if (n % i == 0 && VerifyPeeks(a, (int) (n / i))) return (int) (n / i);
            }
            return 0;
        }

        private static bool VerifyPeeks(int[] a, int d)
        {
            var i = 1;
            var from = 0;
            var to = a.Length / d * i - 1;

            while (to < a.Length)
            {
                if (!VerifyPeek(a, from, to)) return false;

                i++;
                to = a.Length / d * i - 1;
                from = to - a.Length/d + 1;
            }
            return true;
        }

        private static bool VerifyPeek(int[] a, int from, int to)
        {
            if (from == 0) from++;
            for (var i = from; i <= to && i < a.Length - 1; i++)
            {
                if (a[i - 1] < a[i] && a[i] > a[i + 1]) return true;
            }
            return false;
        }
    }
}