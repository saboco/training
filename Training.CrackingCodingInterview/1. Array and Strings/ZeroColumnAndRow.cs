using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class ZeroColumnAndRow
    {
        public static void Zero(int[][] m)
        {
            if (m == null)
            { return; }

            var rc = new HashSet<(int, int)>();
            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < m[i].Length; j++)
                {
                    if (m[i][j] == 0)
                    {
                        rc.Add((i, j));
                    }
                }
            }

            foreach (var (i, j) in rc)
            {
                for (var k = 0; k < m[i].Length; k++)
                {
                    m[i][k] = 0;
                }
                for (var l = 0; l < m.Length; l++)
                {
                    m[l][j] = 0;
                }
            }
        }

        public static void Zero2(int[][] m)
        {
            if (m == null)
            { return; }

            var r = new int[m.Length];
            var c = new int[m[0].Length];
            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < m[i].Length; j++)
                {
                    if (m[i][j] == 0)
                    {
                        r[i] = 1;
                        c[j] = 1;
                    }
                }
            }

            for (var i = 0; i < m.Length; i++)
            {
                if (r[i] == 1)
                {
                    for (var j = 0; j < m[i].Length; j++)
                    {
                        m[i][j] = 0;
                    }
                }
            }
            for (var j = 0; j < m[0].Length; j++)
            {
                if (c[j] == 1)
                {
                    for (var i = 0; i < m.Length; i++)
                    {
                        m[i][j] = 0;
                    }
                }
            }
        }

        public static void Zero3(int[][] m)
        {
            if (m == null)
            { return; }

            var r = new int[m.Length];
            var c = new int[m[0].Length];
            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < m[i].Length; j++)
                {
                    if (m[i][j] == 0)
                    {
                        r[i] = 1;
                        c[j] = 1;
                    }
                }
            }

            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < m[0].Length; j++)
                {
                    if (r[i] == 1 || c[j] == 1)
                    {
                        m[i][j] = 0;
                    }

                }
            }
        }
    }
}