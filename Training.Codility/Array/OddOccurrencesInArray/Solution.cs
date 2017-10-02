namespace Training.Codility.Array.OddOccurrencesInArray
{
    public class Solution
    {
        public static int Solve(int[] arr)
        {
            var result = arr[0];
            for (var i = 1; i < arr.Length; i++)
            {
                result ^= arr[i];
            }
            return result;
        }
    }
}