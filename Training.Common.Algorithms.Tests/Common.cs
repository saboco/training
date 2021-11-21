using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class Common
    {

        public static void AssertEqual(int[][] expected, int[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                AssertEqual(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(double[][] expected, double[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                AssertEqual(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(double[,] expected, double[,] actual)
        {
            Assert.Equal(expected.GetLength(0), actual.GetLength(0));
            Assert.Equal(expected.GetLength(1), actual.GetLength(1));

            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.Equal(expected[i, j], actual[i, j]);
                }
            }
        }
        public static void AssertEqual(int[] expected, int[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(double[] expected, double[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(bool[] expected, bool[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(string[][] expected, string[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                AssertEqual(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(string[] expected, string[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual((int, int)[] expected, (int, int)[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        public static double[,] CreateEmptyAdjacencyMatrix(int n)
        {
            var m = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    m[i, j] = double.PositiveInfinity;
                    m[i, i] = 0;
                }
            }
            return m;
        }
        public static (int, int)[][] GetAdjacencyList(int n, int[][] g)
        {
            var nodes = new List<int[]>();
            var distances = new List<int[]>();
            var zipped = new List<(int, int)[]>();
            for (var i = 0; i < n; i++)
            {
                nodes.Add(g[i]);
            }
            for (var i = n; i < n * 2; i++)
            {
                distances.Add(g[i]);
            }

            for (var i = 0; i < n; i++)
            {
                var zip = new List<(int, int)>();
                for (var j = 0; j < nodes[i].Length; j++)
                {
                    zip.Add((nodes[i][j], distances[i][j]));
                }
                zipped.Add(zip.ToArray());
            }
            return zipped.ToArray();
        }

        public static List<List<int>> CreateEmptyGraph(int n)
        {
            var g = new List<List<int>>(n);
            for (var i = 0; i < n; i++)
            {
                g.Add(new List<int>());
            }
            return g;
        }

        public static void AddUndirectedEdge(List<List<int>> g, int from, int to)
        {
            g[from].Add(to);
            g[to].Add(from);
        }

        public static void AddDrectedEdge(List<List<int>> g, int from, int to)
        {
            g[from].Add(to);
        }
    }
}
