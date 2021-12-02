using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public abstract class NetworkFlowSolver
    {
        protected static long Infinity = Int64.MaxValue / 2;

        protected int NumberOfNodes { get; set; }
        protected int Source { get; set; }
        protected int Sink { get; set; }

        protected int visitedToken = 1;
        protected int[] visited;

        protected bool Solved { get; set; }

        protected long MaxFlow { get; set; }

        protected List<FlowEdge>[] Graph { get; set; }

        public NetworkFlowSolver(int n, int s, int t)
        {
            NumberOfNodes = n;
            Source = s;
            Sink = t;
            InitializeEmptyFlowGraph();
            visited = new int[n];
        }

        public void AddEdge(int from, int to, long capacity)
        {
            if (capacity <= 0)
            { throw new ArgumentException("Forward edge capacity should be greater than 0"); }

            var e1 = new FlowEdge(from, to, capacity);
            var e2 = new FlowEdge(to, from, 0);
            e1.Residual = e2;
            e2.Residual = e1;
            Graph[from].Add(e1);
            Graph[to].Add(e2);
        }

        public List<FlowEdge>[] GetResidualGraph()
        {
            Execute();
            return Graph;
        }

        public long GetMaxFlow()
        {
            Execute();
            return MaxFlow;
        }

        public abstract void Solve();

        public void Visit(int i)
        {
            visited[i] = visitedToken;
        }

        public bool HasBeenVisited(int i)
        {
            return visited[i] == visitedToken;
        }

        public void MarkAllNodesAsUnvisited()
        {
            visitedToken++;
        }

        private void Execute()
        {
            if (Solved) { return; }
            Solved = true;
            Solve();
        }

        private void InitializeEmptyFlowGraph()
        {
            Graph = new List<FlowEdge>[NumberOfNodes];
            for (var i = 0; i < NumberOfNodes; i++)
            {
                Graph[i] = new List<FlowEdge>();
            }
        }
    }
}
