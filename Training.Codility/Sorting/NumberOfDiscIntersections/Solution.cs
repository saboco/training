using Training.DataStructures;
using Training.DataStructures.Sorting;

namespace Training.Codility.Sorting.NumberOfDiscIntersections
{
    public class Solution
    {
        public static int Solve(long[] arr)
        {
            var leftBound = new long[arr.Length];
            var rightBound = new long[arr.Length];

            for (var i = 0; i < arr.Length; i++)
            {
                rightBound[i] = i + arr[i];
                leftBound[i] = i - arr[i];
            }

            Quick.Sort(leftBound);
            Quick.Sort(rightBound);

            var count = 0;
            var j = 0;
            for (var i = 0; i < arr.Length; i++)
                while (j < arr.Length && rightBound[i] >= leftBound[j])
                {
                    count += j - i;
                    j++;
                    if (count > 1e7) return -1;
                }

            return count;
        }
    }
}