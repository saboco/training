using System.Collections.Generic;

namespace Training.Codility.StacksAndQueues.StoneWall
{
    public class Solution
    {
        public static int Solve(int[] heights)
        {
            var stones = new Stack<int>();
            var fixedStones = new Stack<int>();
            var currentHeight = 0;
            for (var i = 0; i < heights.Length; i++)
            {
                if (stones.Count == 0)
                {
                    stones.Push(heights[i]);
                    currentHeight = heights[i];
                    continue;
                }
                currentHeight = BuildWall(heights[i], currentHeight, stones, fixedStones);
            }

            return stones.Count + fixedStones.Count;
        }

        private static int BuildWall(int newHeight, int currentHeight, Stack<int> stones, Stack<int> fixedStones)
        {
            if(newHeight == currentHeight)
            {
                return newHeight;
            }

            while (stones.Count > 0 && currentHeight > newHeight)
            {
                currentHeight -= stones.Peek();
                fixedStones.Push(stones.Pop());
            }
            if (newHeight == currentHeight)
            {
                return newHeight;
            }

            if (stones.Count == 0 || newHeight - currentHeight < 0)
            {
                //Transfer(tempStack, stoneWall);
                stones.Push(newHeight);
                return newHeight;
            }
            //Transfer(tempStack, stoneWall);
            stones.Push(newHeight - currentHeight);
            return newHeight;
        }

        private static void Transfer(Stack<int> from, Stack<int> to)
        {
            while (from.Count > 0)
            {
                to.Push(from.Pop());
            }
        }
    }
}