using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class PointsInSameLine
    {
        public class Line
        {
            public double M { get; }
            public double C { get; }

            private const double epsilon = 0.0001;

            public Line(Point p1, Point p2)
            {
                if (Math.Abs(p1.X - p2.X) > epsilon)
                {
                    M = (p2.Y - p1.Y) / (p2.X - p1.X);
                    C = p2.Y - (M * p2.X);
                }
                else
                {
                    C = p2.X;
                    M = double.PositiveInfinity;
                }
            }

            public bool IsEqual(double a, double b)
            {
                return Math.Abs(a - b) < epsilon;
            }

            public override int GetHashCode()
            {
                var m = (int)(M * 1000);
                var c = (int)(C * 1000);
                return m | c;
            }
            public override bool Equals(object obj)
            {
                var l = (Line)obj;
                return IsEqual(l.M, M) && IsEqual(l.C, C);
            }
        }

        public struct Point
        {
            public double X { get; }
            public double Y { get; }
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public static (Line, int) CountMaxPoints(Point[] points)
        {
            var count = new Dictionary<Line, HashSet<Point>>();
            var max = 0;
            Line bestLine = null;
            for (var i = 0; i < points.Length; i++)
            {
                for (var j = i + 1; j < points.Length; j++)
                {
                    var line = new Line(points[i], points[j]);
                    if (!count.ContainsKey(line))
                    {
                        count[line] = new HashSet<Point> { points[i], points[j] };
                    }
                    else
                    {
                        count[line].Add(points[i]);
                        count[line].Add(points[j]);
                    }
                    if (count[line].Count > max)
                    {
                        bestLine = line;
                        max = count[line].Count;
                    }
                }
            }

            return (bestLine, max);
        }
    }
}
