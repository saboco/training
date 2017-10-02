namespace Training.Codility.Leader.Dominator
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            var leaderInfo = Leader.Find(a, 0, a.Length);
            return leaderInfo[3];
        }
    }
}