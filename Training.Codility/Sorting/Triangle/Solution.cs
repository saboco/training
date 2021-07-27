using Training.DataStructures;
using Training.DataStructures.Sorting;

namespace Training.Codility.Sorting.Triangle
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            if (a.Length < 2)
            {
                return 0;
            }

            var sortedA = Quick.Sort(a);
            for (var i = 2; i < sortedA.Length; i++)
            {
                if (IsTriangularTriplet(sortedA[i - 2], sortedA[i - 1], sortedA[i]))
                {
                    return 1;
                }
            }
            return 0;
        }

        private static bool IsTriangularTriplet(long p, long q, long r)
        {
            return r < p + q && p < q + r && q < r + p;
        }
    }
}