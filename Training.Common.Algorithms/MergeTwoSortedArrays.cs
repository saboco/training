namespace Training.Common.Algorithms
{
    public static class MergeTwoSortedArrays
    {
        public static int[] MergeInOrder(int[] arr1, int[] arr2)
        {
            var n = arr1.Length;
            var m = arr2.Length;
            var nm = n + m;
            var arr = new int[nm];

            for (int i = 0, j = 0, k = 0; i < nm; i++)
            {
                if (k >= m && j < n)
                {
                    arr[i] = arr1[j];
                    j++;
                    continue;
                }

                if (j >= n && k < m)
                {
                    arr[i] = arr2[k];
                    k++;
                    continue;
                }

                if (arr1[j] < arr2[k])
                {
                    arr[i] = arr1[j];
                    j++;
                }
                else
                {
                    arr[i] = arr2[k];
                    k++;
                }
            }

            return arr;
        }
    }
}