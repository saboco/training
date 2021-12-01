using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class ElementaryMathProblem
    {
        // given some pairs return the operators so the results are differents
        // 0: Sum, 1: Multiplication, 2: Substraction
        // ex: given [(1,2), (3,4)] -> [0,1] as 1 + 2 = 3 and 3 * 4 = 12, and 3 <> 12
        public static int[] GetOperationsSoResultIsDifferent((int, int)[] pairs)
        {
            // bipartite graph
            // for [(1,2), (3,4)]
            // operations - results
            // +              3
            // *              2
            // -              1
            // +              7
            // *              12
            // -              1

            var results = new Dictionary<int, HashSet<int>>(); // (result, node ids)
            for (var i = 0; i < pairs.Length; i++)
            {
                var (a, b) = pairs[i];
                var sum = a + b;
                if (!results.ContainsKey(sum))
                {
                    results[sum] = new HashSet<int> { i };
                }
                else
                {
                    results[sum].Add(i);
                }
                var multiplication = a * b;
                if (!results.ContainsKey(multiplication))
                {
                    results[multiplication] = new HashSet<int> { i };
                }
                else
                {
                    results[multiplication].Add(i);
                }
                var substraction = a - b;
                if (!results.ContainsKey(substraction))
                {
                    results[substraction] = new HashSet<int> { i };
                }
                else
                {
                    results[substraction].Add(i);
                }
            }

            var N = pairs.Length + results.Count + 2;
            var S = N - 1;
            var T = N - 2;

            var solver = new MaxFlowFordFulkerson(N, S, T);
            for (var i = 0; i < pairs.Length; i++)
            {
                solver.AddEdge(S, i, 1);
            }

            var id = pairs.Length;
            var resultsById = new Dictionary<int, int>(); // id, result
            foreach (var kv in results)
            {
                foreach (var pairId in kv.Value)
                {
                    solver.AddEdge(pairId, id, 1);
                }
                solver.AddEdge(id, T, 1);
                resultsById[id] = kv.Key;
                id++;
            }

            var residualGraph = solver.GetResidualGraph();
            if (residualGraph[S].Any(e => !e.IsResidual && e.Flow == 0))
            {
                return Array.Empty<int>();
            }

            var operators = new int[pairs.Length];
            for (var i = 0; i < pairs.Length; i++)
            {
                var choosenId = residualGraph[i].Single(e => e.Flow == 1).To;
                var result = resultsById[choosenId];
                var (a, b) = pairs[i];
                if (a + b == result)
                {
                    operators[i] = 0;
                }
                else if (a * b == result)
                {
                    operators[i] = 1;
                }
                else if (a - b == result)
                {
                    operators[i] = 2;
                }
            }

            return operators;
        }
    }
}
