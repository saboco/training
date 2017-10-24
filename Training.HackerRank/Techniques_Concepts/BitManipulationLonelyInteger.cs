namespace Training.HackerRank.Techniques_Concepts
{
    public static class BitManipulationLonelyInteger
    {
        public static int GetLonleyInteger(int[] arr)
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