using System;
using Training.Common;

namespace Training.DataStructures.Sorting
{
    public class Quick
    {
        public static T[] Sort<T>(T[] arr) where T : IComparable
        {
            RandomHelpers.Suffle(arr);
            RecQuick(arr, 0, arr.Length - 1);
            return arr;
        }

        private static void RecQuick<T>(T[] arr, long lo, long hi) where T : IComparable
        {
            while (true)
            {
                if (hi <= lo)
                {
                    return;
                }

                var partitionIndex = Partition(arr, lo, hi);
                RecQuick(arr, lo, partitionIndex - 1);
                lo = partitionIndex + 1;
            }
        }

        private static long Partition<T>(T[] arr, long lo, long hi) where T : IComparable
        {
            var i = lo;
            var j = hi + 1;

            while (i <= j)
            {
                do
                {
                    i++;
                } while (i <= hi && Less(arr[i], arr[lo]));
                do
                {
                    j--;
                } while (lo <= j && Less(arr[lo], arr[j]));
                if (j <= i)
                {
                    break;
                }

                ArrayHelpers.Swap(arr, i, j);
            }
            ArrayHelpers.Swap(arr, lo, j);
            return j;
        }

        private static bool Less<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) < 0;
        }
    }
}