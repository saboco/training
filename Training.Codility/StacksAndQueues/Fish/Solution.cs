using System.Collections.Generic;

namespace Training.Codility.StacksAndQueues.Fish
{
    public class Solution
    {
        public static int Solve(int[] sizes, int[] directions)
        {
            var n = sizes.Length;
            var upstream = new Stack<int>();
            var downstream = new Stack<int>();
            var alives = new Stack<int>();
            for (var fish = 0; fish < n; fish++)
            {
                if(directions[fish] == 0)
                {
                    upstream.Push(fish);
                }

                if (directions[fish] == 1)
                {
                    downstream.Push(fish);
                }

                Swim(upstream, downstream, alives, sizes);
                if (upstream.Count == 1 && directions[upstream.Peek()] == 0)
                {
                    alives.Push(upstream.Pop());
                }
            }
            return alives.Count + upstream.Count + downstream.Count;
        }

        private static void Swim(Stack<int> upstream, Stack<int> downstream, Stack<int> alives, int[] sizes)
        {
            while (upstream.Count > 0 && downstream.Count > 0)
            {
                if (sizes[upstream.Peek()] > sizes[downstream.Peek()])
                {
                    downstream.Pop();
                }
                else
                {
                    upstream.Pop();
                }
            }
        }
    }
}
