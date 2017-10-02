using System.Collections.Generic;
using System.Linq;

namespace Training.Codility.Leader
{
    public static class Leader
    {
        public static int[] Find(int[] a, int from, int to)
        {
            if (a.Length == 0) return new[] {0, -1, 0, -1};
            if (from == to) return new[] {1, a[from], 1, from};

            var n = to - from;

            var stack = new Stack<int>();

            for (var i = from; i < to; i++)
            {
                if (stack.Count == 0)
                {
                    stack.Push(a[i]);
                    continue;
                }

                if (stack.Peek() != a[i])
                    stack.Pop();
                else
                {
                    stack.Push(a[i]);
                }
            }
            var leader = -1;
            if (stack.Count == 0) return new[] {0, leader, 0, -1};
            var candidate = stack.Peek();
            var count = 0;
            var indexes = new List<int>();
            for (var i = from; i < to; i++)
            {
                if (a[i] == candidate)
                {
                    count++;
                    indexes.Add(i);
                }
            }
            var isLeader = false;
            if (count > n / 2)
            {
                isLeader = true;
                leader = candidate;
            }
            return isLeader ? new[] {1, leader, count, indexes.First()} : new[] {0, leader, count, -1};
        }
    }
}