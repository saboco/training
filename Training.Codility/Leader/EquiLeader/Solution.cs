namespace Training.Codility.Leader.EquiLeader
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            var equiLeadersCount = 0;
            var leaderInfo = Leader.Find(a, 0, a.Length);
            if (leaderInfo[0] == 0)
            {
                return 0;
            }

            var totalCount = leaderInfo[2];
            var leader = leaderInfo[1];
            var leftCount = 0;
            var rightCount = totalCount;
            var n = a.Length;
            for (var i = 0; i < a.Length - 1; i++)
            {
                if (a[i] == leader)
                {
                    leftCount++;
                    rightCount--;
                }
                if (leftCount > (i + 1) / 2 && rightCount > (n - i - 1) / 2)
                {
                    equiLeadersCount++;
                }
            }
            return equiLeadersCount;
        }
    }
}