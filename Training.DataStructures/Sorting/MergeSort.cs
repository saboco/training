namespace Training.DataStructures.Sorting
{
    public static class MergeSort
    {
        public static int[] Sort(int[] arr)
        {
            var aux = new int[arr.Length];
            Clone(arr, aux);
            InternalSort(arr, aux, 0, arr.Length - 1);
            return arr;
        }

        private static void InternalSort(int[] arr, int[] aux, int lo, int hi)
        {
            if (hi <= lo) return;
            var mid = (hi + lo) / 2;
            InternalSort(aux, arr, lo, mid);
            InternalSort(aux, arr, mid + 1, hi);
            Merge(arr, aux, lo, mid, hi);
        }

        public static void Merge(int[] arr, int[] aux, int lo, int mid, int hi)
        {
            var leftIndex = lo;
            var rightIndex = mid + 1;

            for (var k = lo; k <= hi; k++)
            {
                if (mid < leftIndex) arr[k] = aux[rightIndex++];
                else if (hi < rightIndex) arr[k] = aux[leftIndex++];
                else if (aux[rightIndex] < aux[leftIndex]) arr[k] = aux[rightIndex++];
                else arr[k] = aux[leftIndex++];
            }
        }

        private static void Clone(int[] arr, int[] aux)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                aux[i] = arr[i];
            }
        }
    }
}