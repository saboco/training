namespace Training.DataStructures
{
    public class BinarySearch
    {
        public static int Search(int[] arr, int lookFor)
        {
            return InternalSearch(arr, 0, arr.Length, lookFor, false);
        }

        public static int SearchNearest(int[] arr, int lookFor)
        {
            return InternalSearch(arr, 0, arr.Length, lookFor, true);
        }

        private static int InternalSearch(int[] arr, int start, int end, int n, bool nearest)
        {
            var mid = (start + end) / 2;
            var nearesIndex = nearest ? mid : -1;

            if (arr[mid] == n)
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

            if (n < arr[mid])
            {
                return InternalSearch(arr, start, mid, n, nearest);
            }

            if (arr[mid] < n)
            {
                return InternalSearch(arr, mid, end, n, nearest);
            }

            return nearesIndex;
        }
    }
}