using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class TarjansStronglyConnectedComponents
    {
        private const int _unvisited = -1;

        public static (int, int[]) FindStronglyConnectedComponents(int[][] g)
        {
            var low = new int[g.Length];
            var ids = new int[g.Length];
            var stack = new Stack<int>();
            var onStack = new bool[g.Length];
            var sccCount = 0;
            var id = 0;

            Array.Fill(ids, _unvisited);

            for (var i = 0; i < g.Length; i++)
            {
                if (ids[i] == _unvisited)
                {
                    sccCount = Dfs(g, i, i, ids, low, stack, onStack, sccCount);
                }
            }

            return (sccCount, low);
        }

        private static int Dfs(int[][] g, int at, int id, int[] ids, int[] low, Stack<int> stack, bool[] onStack, int sccCount)
        {
            stack.Push(at);
            onStack[at] = true;
            low[at] = id;
            ids[at] = id;

            foreach (var to in g[at])
            {
                if (ids[to] == _unvisited)
                {
                    sccCount = Dfs(g, to, id + 1, ids, low, stack, onStack, sccCount);
                }

                if (onStack[to])
                {
                    low[at] = Math.Min(low[at], low[to]);
                }
            }
            if (ids[at] == low[at])
            {
                for (var node = stack.Pop(); ; node = stack.Pop())
                {
                    onStack[node] = false;
                    low[node] = ids[at];

                    if (node == at)
                    { break; }
                }
                sccCount++;
            }

            return sccCount;
        }
    }
}
