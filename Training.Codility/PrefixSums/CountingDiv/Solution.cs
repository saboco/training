namespace Training.Codility.PrefixSums.CountingDiv
{
    public class Solution
    {
        public static int Solve(int a, int b, int k)
        {
            if (k == 1) return b - a + 1;
            var countZero = a == 0 ? 1 : 0;
            return (b / k - (a - 1) / k) + countZero;
        }
    }
}