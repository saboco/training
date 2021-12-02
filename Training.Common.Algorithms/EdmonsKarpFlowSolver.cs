using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class EdmonsKarpFlowSolver : NetworkFlowSolver
    {
        public EdmonsKarpFlowSolver(int n, int s, int t) : base(n, s, t)
        {
        }

        public override void Solve()
        {
            long flow;
            do
            {
                MarkAllNodesAsUnvisited();
                flow = Bfs();
                MaxFlow += flow;
            } while (flow != 0);
        }

        private long Bfs()
        {
            var q = new Queue<int>();
            Visit(Source);
            q.Enqueue(Source);

            var prev = new FlowEdge[NumberOfNodes];
            while (q.Count > 0)
            {
                var node = q.Dequeue();
                if (node == Sink) { break; }
                foreach (var edge in Graph[node])
                {
                    var capacity = edge.RemainingCapacity;
                    if (capacity > 0 && !HasBeenVisited(edge.To))
                    {
                        Visit(edge.To);
                        prev[edge.To] = edge;
                        q.Enqueue(edge.To);
                    }
                }
            }

            if (prev[Sink] == null) { return 0; }
            var bottleNeck = long.MaxValue;
            for (var edge = prev[Sink]; edge != null; edge = prev[edge.From])
            {
                bottleNeck = Math.Min(bottleNeck, edge.RemainingCapacity);
            }

            for (var edge = prev[Sink]; edge != null; edge = prev[edge.From])
            {
                edge.Augment(bottleNeck);
            }

            return bottleNeck;
        }
    }
}
