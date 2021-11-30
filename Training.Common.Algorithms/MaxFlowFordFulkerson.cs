using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class Edge
    {
        public int From { get; }
        public int To { get; }
        public long Flow { get; set; }
        public long Capacity { get; set; }

        public bool IsResidual => Capacity == 0;
        public long RemainingCapacity => Capacity - Flow;
        public Edge Residual { get; set; }

        public Edge(int from, int to, long capacity)
        {
            From = from;
            To = to;
            Capacity = capacity;
        }
        public void Augment(long bottleNeck)
        {
            Flow += bottleNeck;
            Residual.Flow -= bottleNeck;
        }

        public string Print(int s, int t)
        {
            var u = (From == s) ? "s" : ((From == t) ? "t" : From.ToString());
            var v = (To == s) ? "s" : ((To == t) ? "t" : To.ToString());
            return $"Edge {u} -> {v} | flow = {Flow} |  capacity = {Capacity} | is residual: {IsResidual}";
        }
    }

    public abstract class NetworkFlowSolver
    {
        protected static long Infinity = Int64.MaxValue / 2;

        protected int numberOfNodes { get; set; }
        protected int source { get; set; }
        protected int sink { get; set; }

        protected int visitedToken = 1;
        protected int[] visited;

        protected bool solved { get; set; }

        protected long maxFlow { get; set; }

        protected List<Edge>[] graph { get; set; }

        public NetworkFlowSolver(int n, int s, int t)
        {
            numberOfNodes = n;
            source = s;
            sink = t;
            InitializeEmptyFlowGraph();
            visited = new int[n];
        }

        public void AddEdge(int from, int to, long capacity)
        {
            if (capacity <= 0)
            { throw new ArgumentException("Forward edge capacity should be greater than 0"); }

            var e1 = new Edge(from, to, capacity);
            var e2 = new Edge(to, from, 0);
            e1.Residual = e2;
            e2.Residual = e1;
            graph[from].Add(e1);
            graph[to].Add(e2);
        }

        public List<Edge>[] GetResidualGraph()
        {
            Execute();
            return graph;
        }

        public long GetMaxFlow()
        {
            Execute();
            return maxFlow;
        }

        private void Execute()
        {
            if(solved){ return; }
            solved = true;
            Solve();
        }

        public abstract void Solve();

        private void InitializeEmptyFlowGraph()
        {
            graph = new List<Edge>[numberOfNodes];
            for (var i = 0; i < numberOfNodes; i++)
            {
                graph[i] = new List<Edge>();
            }
        }
    }
    public class MaxFlowFordFulkerson : NetworkFlowSolver
    {
        public MaxFlowFordFulkerson(int n, int s, int t) : base(n, s, t)
        {
        }

        public override void Solve()
        {
            for(var f = Dfs(source, Infinity); f != 0; f = Dfs(source, Infinity))
            { 
                visitedToken++;
                maxFlow+=f;
            }
        }

        private long Dfs(int node, long flow)
        {
            if(node == sink)
            {
                return flow;
            }

            visited[node] = visitedToken;
            
            foreach(var edge in graph[node])
            {
                if(edge.RemainingCapacity > 0 && visited[edge.To] != visitedToken)
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
