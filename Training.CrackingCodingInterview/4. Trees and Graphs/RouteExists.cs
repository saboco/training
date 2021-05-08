using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class RouteExistsGraph
    {
        private List<List<int>> _nodes = new List<List<int>>();

        public RouteExistsGraph(int nodes)
        {
            for (var i = 0;i < nodes; i++)
            {
                _nodes.Add(new List<int>());
            }
        }

        public void AddEdge(int src, int dst)
        { 
            _nodes[src].Add(dst);
        }

        public bool IsReachableFrom(int src, int dst)
        { 
            var visited = new HashSet<int>();
            return Dfs(src, dst, visited);
        }

        private bool Dfs(int n, int dst, HashSet<int> visited)
        { 
            visited.Add(n);
            foreach(var i in _nodes[n])
            {
                if(i==dst){return true; }
                if(!visited.Contains(i))
                {
                    Dfs(i, dst, visited);
                }
            }
            return false;
        }
    }
}
