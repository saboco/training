using System;

namespace Training.Common.Algorithms
{
    public static class BinarySearch
    {
        public static int Search<T>(T[] arr, T lookFor) where T : IComparable
        {
            return InternalSearch(arr, 0, arr.Length, lookFor, false);
        }

        public static int SearchNearest<T>(T[] arr, T lookFor) where T : IComparable
        {
            return InternalSearch(arr, 0, arr.Length, lookFor, true);
        }

        private static int InternalSearch<T>(T[] arr, int start, int end, T n, bool nearest) where T : IComparable
        {
            var mid = start + ((end - start) / 2); // doing this way rather than (start + end)/2 to prevent overflow of int
            var nearesIndex = nearest ? mid : -1;

            if (arr[mid].CompareTo(n) == 0)
            {
                return mid;
            }

            if (mid == start)
            {
                return nearesIndex;
            }

            if (mid == end)
            {
                return nearesIndex;
            }

            if (0 < arr[mid].CompareTo(n))
            {
                return InternalSearch(arr, start, mid, n, nearest);
            }

            if (arr[mid].CompareTo(n) < 0)
            {
                return InternalSearch(arr, mid, end, n, nearest);
            }

            return nearesIndex;
        }
    }
}