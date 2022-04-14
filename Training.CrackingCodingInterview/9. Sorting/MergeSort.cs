using System;

namespace Training.CrackingCodingInterview
{
    public class MergeSort
    {
        public static void Sort(int[] arr)
        {
            var tmp = new int[arr.Length];
            Divide(arr, tmp, 0, arr.Length - 1);
        }

        private static void Divide(int[] arr, int[] tmp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
            { return; }

            var mid = (leftStart + rightEnd) / 2;
            Divide(arr, tmp, leftStart, mid);
            Divide(arr, tmp, mid + 1, rightEnd);
            Merge(arr, tmp, leftStart, rightEnd);
        }

        private static void Merge(int[] arr, int[] tmp, int leftStart, int rightEnd)
        {
            var leftEnd = (leftStart + rightEnd) / 2;
            var rightStart = leftEnd + 1;
            var size = rightEnd - leftStart + 1;

            var left = leftStart;
            var right = rightStart;
            var index = leftStart;

            while (left <= leftEnd && right <= rightEnd)
            {
                if (arr[left] <= arr[right])
                {
                    tmp[index] = arr[left];
                    left++;
                }
                else
                {
                    tmp[index] = arr[right];
                    right++;
                }
                index++;
            }
            Array.Copy(arr, left, tmp, index, leftEnd - left + 1);
            Array.Copy(arr, right, tmp, index, rightEnd - right + 1);
            Array.Copy(tmp, leftStart, arr, leftStart, size);
        }
    }
}
