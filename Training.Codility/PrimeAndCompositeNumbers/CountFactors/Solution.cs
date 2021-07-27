namespace Training.Codility.PrimeAndCompositeNumbers.CountFactors
{
    public class Solution
    {
        public static int Solve(int n)
        {
            var i = 1L;
            var count = 0;
            while (i * i < n)
            {
                if (n % i == 0)
                {
                    count += 2;
                }

                i++;
            }
            if (i * i == n)
            {
                count++;
            }

            return count;
        }
    }
}