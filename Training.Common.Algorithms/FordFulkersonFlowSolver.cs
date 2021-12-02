using System;

namespace Training.Common.Algorithms
{
    public class FordFulkersonFlowSolver : NetworkFlowSolver
    {
        public FordFulkersonFlowSolver(int n, int s, int t) : base(n, s, t)
        {
        }

        public override void Solve()
        {
            for(var f = Dfs(Source, Infinity); f != 0; f = Dfs(Source, Infinity))
            { 
                MarkAllNodesAsUnvisited();
                MaxFlow+=f;
            }
        }

        private long Dfs(int node, long flow)
        {
            if(node == Sink)
            {
                return flow;
            }

            Visit(node);
            
            foreach(var edge in Graph[node])
            {
                if(edge.RemainingCapacity > 0 && !HasBeenVisited(edge.To))
                {
                    var bottleNeck = Dfs(edge.To, Math.Min(flow, edge.RemainingCapacity));

                    if(bottleNeck > 0)
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
