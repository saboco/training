namespace Training.Codility.Array.CyclicRotation
{
    public class Solution
    {
        public static int[] Solve(int[] arr, int n)
        {
            var aux = new int[arr.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                aux[(i + n) % arr.Length] = arr[i];
            }
            return aux;
        }
    }
}
