using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class DungeonScape
    {
        public static int ShortestWayOut(char[,] dungeon)
        {
            var N = dungeon.GetLength(0);
            var M = dungeon.GetLength(1);

            int startR = 0, startC = 0;
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < M; j++)
                {
                    if (dungeon[i, j] == 'S')
                    {
                        startR = i;
                        startC = j;
                        i = dungeon.GetLength(0);
                        j = dungeon.GetLength(1);
                    }
                }
            }

            var toVisit = new Queue<(int, int)>();
            var visited = new bool[N, M];
            var prev = new (int, int)[N, M];
            var dr = new[] { -1, 1, 0, 0 };
            var dc = new[] { 0, 0, 1, -1 };

            toVisit.Enqueue((startR, startC));
            visited[startR, startC] = true;
            int endR = -1, endC = -1;
            var reachedEnd = false;

            while (toVisit.Count > 0)
            {
                var (r, c) = toVisit.Dequeue();

                for (var i = 0; i < 4; i++)
                {
                    var rr = r + dr[i];
                    var cc = c + dc[i];
                    if (rr < 0 || cc < 0)
                    { continue; }
                    if (rr >= N || cc >= M)
                    { continue; }

                    if (dungeon[rr, cc] == '.' && !visited[rr, cc])
                    {
                        toVisit.Enqueue((rr, cc));
                        visited[rr, cc] = true;
                        prev[rr, cc] = (r, c);
                    }
                    else if (dungeon[rr, cc] == 'E')
                    {
                        endR = rr;
                        endC = cc;
                        prev[rr, cc] = (r, c);
                        toVisit.Clear();
                        reachedEnd = true;
                        break;
                    }
                }
            }
            var count = 0;

            {
                int r = endR, c = endC;

                while (r != startR || c != startC)
                {
                    (r, c) = prev[r, c];
                    count++;
                }
            }
            return reachedEnd ? count : -1;
        }
    }
}
