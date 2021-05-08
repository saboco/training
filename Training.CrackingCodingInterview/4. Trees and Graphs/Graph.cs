using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class Graph
    {
        private List<int>[] _vertices;

        public Graph(int numberOfVertices)
        {

            _vertices = new List<int>[numberOfVertices];
            for (var i = 0; i < numberOfVertices; i++)
            {
                _vertices[i] = new List<int>();
            }
        }

        public void AddEdge(int v, int w)
        {
            _vertices[v].Add(w);
        }

        public void Dfs(int v, Action<int> f)
        {
            var visited = new HashSet<int>();
            Dfs(v, visited, f);
            for (var i = 0; i < _vertices.Length; i++)
            {
                if (!visited.Contains(i))
                {
                    Dfs(i, visited, f);
                }
            }
        }

        private void Dfs(int v, HashSet<int> visited, Action<int> f)
        {
            f(v);
            visited.Add(v);
            foreach (var child in _vertices[v])
            {
                if (!visited.Contains(child))
                {
                    Dfs(child, visited, f);
                }
            }
        }

        public void Bfs(int v, Action<int> f)
        {
            var visited = new HashSet<int>();
            Bfs(v, visited, f);

            for(var i = 0; i < _vertices.Length; i++)
            { 
                if(!visited.Contains(i))
                { 
                    Bfs(i, visited, f);
                }
            }
        }

        private void Bfs(int v, HashSet<int> visited, Action<int> f)
        {
            var queue = new Queue<int>();
            visited.Add(v);
            queue.Enqueue(v);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                f(current);
                foreach (var i in _vertices[current])
                {
                    if (!visited.Contains(i))
                    {
                        visited.Add(i);
                        queue.Enqueue(i);
                    }
                }
            }
        }
    }
}
