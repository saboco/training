namespace Training.HackerRank.Techniques_Concepts
{
    public static class TimeComplexityPrimality
    {
        public static bool IsPrime(int n)
        {
            if (n == 1)
            {
                return false;
            }

            var i = 2;
            while (i * i <= n)
            {
                if (n % i == 0)
                {
                    return false;
                }

                i++;
            }
            return true;
        }
    }
}