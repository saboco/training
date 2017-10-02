using Training.DataStructures;
using Training.DataStructures.Sorting;

namespace Training.Codility.Sorting.Distinct
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            if (a.Length == 0) return 0;

            var sortedA = Quick.Sort(a);
            var distinct = 1; // there is at least one value so there is at least one distinct value
            for (var i = 1; i < sortedA.Length; i++)
            {
                if (sortedA[i] != sortedA[i - 1])
                    distinct++;
            }
            return distinct;
        }
    }
}