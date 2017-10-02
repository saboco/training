namespace Training.Common
{
    public static class ArrayHelpers
    {
        public static void Swap<T>(T[] arr, long from, long to)
        {
            var tempVal = arr[from];
            arr[from] = arr[to];
            arr[to] = tempVal;
        }
    }
}