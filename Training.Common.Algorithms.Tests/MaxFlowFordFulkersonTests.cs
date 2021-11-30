﻿using System.Text;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class MaxFlowFordFulkersonTests
    {
        [Fact]
        public void MaxFlow()
        {
            var n = 12;
            var s = n - 2;
            var t = n - 1;

            var solver = new MaxFlowFordFulkerson(n, s, t);

            solver.AddEdge(s, 0, 10);
            solver.AddEdge(s, 1, 5);
            solver.AddEdge(s, 2, 10);

            solver.AddEdge(0, 3, 10);
            solver.AddEdge(1, 2, 10);
            solver.AddEdge(2, 5, 15);
            solver.AddEdge(3, 1, 2);
            solver.AddEdge(3, 6, 15);
            solver.AddEdge(4, 1, 15);
            solver.AddEdge(4, 3, 3);
            solver.AddEdge(5, 4, 4);
            solver.AddEdge(5, 8, 10);
            solver.AddEdge(6, 7, 10);
            solver.AddEdge(7, 4, 10);
            solver.AddEdge(7, 5, 7);

            solver.AddEdge(6, t, 15);
            solver.AddEdge(8, t, 10);

            Assert.Equal(23, solver.GetMaxFlow());
            var residualGraph = solver.GetResidualGraph();
            var sb = new StringBuilder();
            foreach (var edges in residualGraph)
            {
                foreach (var edge in edges)
                {
                    sb.AppendLine(edge.Print(s, t));
                }
            }
            var expected = "Edge 0 -> s | flow = -10 |  capacity = 0 | is residual: True\r\nEdge 0 -> 3 | flow = 10 |  capacity = 10 | is residual: False\r\nEdge 1 -> s | flow = -5 |  capacity = 0 | is residual: True\r\nEdge 1 -> 2 | flow = 5 |  capacity = 10 | is residual: False\r\nEdge 1 -> 3 | flow = 0 |  capacity = 0 | is residual: True\r\nEdge 1 -> 4 | flow = 0 |  capacity = 0 | is residual: True\r\nEdge 2 -> s | flow = -8 |  capacity = 0 | is residual: True\r\nEdge 2 -> 1 | flow = -5 |  capacity = 0 | is residual: True\r\nEdge 2 -> 5 | flow = 13 |  capacity = 15 | is residual: False\r\nEdge 3 -> 0 | flow = -10 |  capacity = 0 | is residual: True\r\nEdge 3 -> 1 | flow = 0 |  capacity = 2 | is residual: False\r\nEdge 3 -> 6 | flow = 13 |  capacity = 15 | is residual: False\r\nEdge 3 -> 4 | flow = -3 |  capacity = 0 | is residual: True\r\nEdge 4 -> 1 | flow = 0 |  capacity = 15 | is residual: False\r\nEdge 4 -> 3 | flow = 3 |  capacity = 3 | is residual: False\r\nEdge 4 -> 5 | flow = -3 |  capacity = 0 | is residual: True\r\nEdge 4 -> 7 | flow = 0 |  capacity = 0 | is residual: True\r\nEdge 5 -> 2 | flow = -13 |  capacity = 0 | is residual: True\r\nEdge 5 -> 4 | flow = 3 |  capacity = 4 | is residual: False\r\nEdge 5 -> 8 | flow = 10 |  capacity = 10 | is residual: False\r\nEdge 5 -> 7 | flow = 0 |  capacity = 0 | is residual: True\r\nEdge 6 -> 3 | flow = -13 |  capacity = 0 | is residual: True\r\nEdge 6 -> 7 | flow = 0 |  capacity = 10 | is residual: False\r\nEdge 6 -> t | flow = 13 |  capacity = 15 | is residual: False\r\nEdge 7 -> 6 | flow = 0 |  capacity = 0 | is residual: True\r\nEdge 7 -> 4 | flow = 0 |  capacity = 10 | is residual: False\r\nEdge 7 -> 5 | flow = 0 |  capacity = 7 | is residual: False\r\nEdge 8 -> 5 | flow = -10 |  capacity = 0 | is residual: True\r\nEdge 8 -> t | flow = 10 |  capacity = 10 | is residual: False\r\nEdge s -> 0 | flow = 10 |  capacity = 10 | is residual: False\r\nEdge s -> 1 | flow = 5 |  capacity = 5 | is residual: False\r\nEdge s -> 2 | flow = 8 |  capacity = 10 | is residual: False\r\nEdge t -> 6 | flow = -13 |  capacity = 0 | is residual: True\r\nEdge t -> 8 | flow = -10 |  capacity = 0 | is residual: True\r\n";
            var actual = sb.ToString();
            Assert.Equal(expected, actual);

        }
    }
}
