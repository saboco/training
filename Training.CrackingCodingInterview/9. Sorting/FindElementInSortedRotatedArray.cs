namespace Training.CrackingCodingInterview
{
    public class FindElementInSortedRotatedArray
    {
        public static int Find(int[] arr, int elem)
        {
            return Find(arr, elem, 0, arr.Length - 1);
        }

        private static int Find(int[] arr, int elem, int lo, int hi)
        {
            if (lo > hi)
            {
                return -1;
            }

            var mid = (lo + hi) / 2;
            if (arr[mid] == elem)
            { return mid; }

            return arr[lo] < arr[mid] && arr[lo] <= elem && elem < arr[mid]
                || arr[lo] > arr[mid] && arr[mid] > elem
                ? Find(arr, elem, lo, mid)
                : Find(arr, elem, mid + 1, hi);
        }
    }
}
