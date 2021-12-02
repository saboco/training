using System;

namespace Training.Common.Algorithms
{
    public class CapacityScalingFlowSolver : NetworkFlowSolver
    {
        public CapacityScalingFlowSolver(int n, int s, int t) : base(n, s, t)
        {
        }

        public override void Solve()
        {
            var delta = FindDelta();
            for (long f; delta > 0; delta /= 2)
            {
                do
                {
                    MarkAllNodesAsUnvisited();
                    f = Dfs(Source, Infinity, delta);
                    MaxFlow += f;
                } while (f != 0);
            }
        }

        private int FindDelta()
        {
            var maxCapacity = FindMaxCapacity(Source, 0);
            MarkAllNodesAsUnvisited();
            
            var delta = 1;
            while (delta * 2 < maxCapacity)
            {
                delta *= 2;
            }
            return delta;
        }

        private long FindMaxCapacity(int n, long maxCapacity)
        {
            Visit(n);
            foreach (var edge in Graph[n])
            {
                if (!HasBeenVisited(edge.To))
                {
                    maxCapacity = FindMaxCapacity(edge.To, Math.Max(maxCapacity, edge.RemainingCapacity));
                }
            }

            return maxCapacity;
        }

        private long Dfs(int n, long flow, int delta)
        {
            if (n == Sink) { return flow; }

            Visit(n);

            foreach (var edge in Graph[n])
            {
                if (edge.RemainingCapacity >= delta && !HasBeenVisited(edge.To))
                {
                    var bottleNeck = Dfs(edge.To, Math.Min(flow, edge.RemainingCapacity), delta);

                    if (bottleNeck > 0)
                    {
                        edge.Augment(bottleNeck);
                        return bottleNeck;
                    }
                }
            }
            return 0;
        }
    }
}
