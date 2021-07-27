using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNode = Training.CrackingCodingInterview.BinaryTree.Node;

namespace Training.CrackingCodingInterview
{
    public class SumPaths
    {
        public static List<string> PrintSumPaths(BNode n, int target)
        {
            var paths = new List<string>();
            var sums = new List<int>();
            SearchSum(n, target, paths, sums, 0);
            return paths;
        }

        private static void SearchSum(BNode n, int target, List<string> paths, List<int> sums, int level)
        {
            if (n == null)
            { return; }
            sums.Add(n.Data);
            var sum = target;
            for (var i = level; i >= 0; i--)
            {
                sum -= sums[i];
                if (sum == 0)
                { paths.Add(GetPath(sums, i, level)); }
            }
            var c1 = Clone(sums);
            var c2 = Clone(sums);
            SearchSum(n.Left, target, paths, c1, level + 1);
            SearchSum(n.Right, target, paths, c2, level + 1);
        }

        private static string GetPath( List<int> sums, int from, int to)
        {
            var sb = new StringBuilder();
            for (var i = from; i <= to; i++)
            {
                sb.Append(sums[i]);
                if (i < to)
                { sb.Append('¤'); }
            }
            return sb.ToString();
        }

        private static List<int> Clone(List<int> list)
        {
            var newList = new List<int>();
            foreach (var i in list)
            {
                newList.Add(i);
            }
            return newList;
        }
    }
}
