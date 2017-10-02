namespace Training.HackerRank.Algorithms
{
    public class CountingInversions
    {
        public static long CountInversions(int[] arr)
        {
            var aux = new int[arr.Length];
            Clone(arr, aux);
            return CountInversions(arr, aux, 0, aux.Length - 1);
        }

        private static long CountInversions(int[] arr, int[] aux, int lo, int hi)
        {
            if (hi <= lo) return 0;
            var mid = (lo + hi) / 2;
            var count = 0L;
            count += CountInversions(aux, arr, lo, mid);
            count += CountInversions(aux, arr, mid + 1, hi);
            return count + Merge(arr, aux, lo, mid, hi);
        }

        private static long Merge(int[] arr, int[] aux, int lo, int mid, int hi)
        {
            var leftIndex = lo;
            var rightIndex = mid + 1;
            var count = 0;

            for (var k = lo; k <= hi; k++)
            {
                if (mid < leftIndex) arr[k] = aux[rightIndex++];
                else if (hi < rightIndex) arr[k] = aux[leftIndex++];
                else if (aux[leftIndex] <= aux[rightIndex]) arr[k] = aux[leftIndex++];
                else
                {
                    arr[k] = aux[rightIndex++];
                    count += mid + 1 - leftIndex;
                }
            }
            return count;
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