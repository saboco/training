using System.Collections.Generic;

namespace Training.Codility.CountingElements.FrogRiverOne
{
    public class Solution
    {
        public static int Solve(int[] leaves, int x)
        {
            var pastLeaves = new HashSet<int>();
            var position = 0;
            for (var i = 0; i < leaves.Length; i++)
            {
                if (leaves[i] == position + 1)
                {
                    position++;
                    while(pastLeaves.Contains(position + 1))
                    {
                        position++;
                        if (position == x) return i;
                    }
                    if (position == x) return i;
                }
                pastLeaves.Add(leaves[i]);
            }
            return -1;
        }
    }
}