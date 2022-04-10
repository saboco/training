using System;

namespace Training.CrackingCodingInterview
{
    public class QuickSort
    {
        public static void Sort(int[] arr)
        {
            Quick(arr, 0, arr.Length - 1);
        }

        private static void Quick(int[] arr, int left, int right)
        {
            if (left >= right) { return; }
            var pivot = arr[(left + right) / 2];
            int index = Partition(arr, left, right, pivot);
            Quick(arr, left, index - 1);
            Quick(arr, index, right);
        }

        private static int Partition(int[] arr, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (arr[left] < pivot)
                { left++; }

                while (arr[right] > pivot)
                { right--; }

                if (left <= right)
                {
                    Swap(arr, left, right);
                    left++;
                    right--;
                }
            }
            return left;
        }

        private static void Swap(int[] arr, int left, int right)
        {
            var tmp = arr[left];
            arr[left] = arr[right];
            arr[right] = tmp;
        }

        private static void Suffle(int[] arr)
        {
            throw new NotImplementedException();
        }
    }
}
