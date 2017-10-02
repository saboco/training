namespace Training.Codility.TimeComplexity.PermMissingElem
{
    public static class Solution
    {
        public static int Solve(int[] arr)
        {
            long n = arr.Length + 1;
            var sum = n * (n + 1) / 2;
            long arrSum = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                arrSum += arr[i];
            }
            return (int)(sum - arrSum);
        }
    }
}