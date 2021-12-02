using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class MiceAndOwls
    {
        public struct Point
        {
            readonly int X;
            readonly int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static bool IsInDistance(Point p1, Point p2, int distance)
            {
                return Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) <= Math.Pow(distance, 2);
            }

        }
        public class Mouse
        {
            public Point Point { get; }
            public Mouse(int x, int y)
            {
                Point = new Point(x, y);
            }
        }

        public class Hole
        {
            public int Capacity { get; }
            public Point Point { get; }
            public Hole(int x, int y, int capacity)
            {
                Capacity = capacity;
                Point = new Point(x, y);
            }
        }

        private readonly Mouse[] _mice;
        private readonly Hole[] _holes;
        private readonly int _radius;
        public MiceAndOwls((int, int)[] m, (int, int, int)[] h, int radius)
        {
            var mice = new List<Mouse>();
            foreach (var (x, y) in m)
            {
                mice.Add(new Mouse(x, y));
            }
            _mice = mice.ToArray();
            var holes = new List<Hole>();
            foreach (var (x, y, capacity) in h)
            {
                holes.Add(new Hole(x, y, capacity));
            }
            _holes = holes.ToArray();
            _radius = radius;
        }

        public long MaxMiceSaved()
        {
            var M = _mice.Length;
            var H = _holes.Length;

            var N = M + H + 2;
            var S = N - 1;
            var T = N - 2;

            var solver = new FordFulkersonFlowSolver(N, S, T);

            for (var i = 0; i < M; i++)
            {
                solver.AddEdge(S, i, 1); // each node represents 1 mouse
            }

            for (var i = 0; i < M; i++)
            {
                var mouse = _mice[i].Point;
                for (var j = 0; j < H; j++)
                {
                    var hole = _holes[j].Point;
                    if (Point.IsInDistance(mouse, hole, _radius))
                    {
                        solver.AddEdge(i, M + j, 1); // one mouse only can choose 1 hole
                    }
                }
            }

            for (var i = 0; i < H; i++)
            {
                solver.AddEdge(M + i, T, _holes[i].Capacity); // each hole can fit Hole.Capacity mice
            }

            return solver.GetMaxFlow();
        }
    }
}
