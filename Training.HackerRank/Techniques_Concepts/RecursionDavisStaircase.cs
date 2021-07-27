using System;
using Training.Common;

namespace Training.HackerRank.Techniques_Concepts
{
    public static class RecursionDavisStaircase
    {
        public static long GetNumberOfWays(int staircaseSize)
        {
            Func<int, long> climb = null;
            climb = (n) =>
            {
                if (n == 0)
                {
                    return 0;
                }

                if (n == 1)
                {
                    return 1;
                }

                if (n == 2)
                {
                    return 2;
                }

                if (n == 3)
                {
                    return 4;
                }

                return climb(n - 1) + climb(n - 2) + climb(n - 3);
            };

            climb = climb.Memoize();
            
            return climb(staircaseSize);
        }
    }
}