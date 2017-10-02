using System;
using Training.DataStructures;
using Training.DataStructures.Sorting;

namespace Training.Codility.Sorting.MaxProductOfThree
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            var sortedA = Quick.Sort(a);
            var max = int.MinValue;
            var maxNegativeProduct = int.MinValue;
            for (var i = 2; i < sortedA.Length; i++)
            {
                if (sortedA[i - 1] < 0)
                    maxNegativeProduct = Math.Max(maxNegativeProduct, GetMaxNegativePair(sortedA, i));
                max = Math.Max(max, TripletProduct(sortedA, i, maxNegativeProduct));
            }
            return max;
        }

        private static int GetMaxNegativePair(int[] sortedA, int i)
        {
            return Math.Max(sortedA[i - 2] * sortedA[i - 1], sortedA[i - 1] * sortedA[i]);
        }

        private static int TripletProduct(int[] arr, int i, int minNegariveProduct)
        {
            var maxProduct = arr[i - 2] * arr[i - 1] * arr[i];
            return Math.Max(maxProduct, minNegariveProduct * arr[i]);
        }
    }
}