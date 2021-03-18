using System;
using System.Collections.Generic;

namespace Training.Leetcode
{
    public class NestedListWeightSum
    {
        public static int DepthSum(IList<NestedInteger> nestedList)
        {
            var sum = 0;
            foreach (var ni in nestedList)
            {
                sum += Dfs(ni, 1);
            }
            return sum;
        }
        private static int Dfs(NestedInteger ni, int depth)
        {
            if (ni.IsInteger())
            {
                return depth * ni.GetInteger();
            }
            var sum = 0;
            foreach (var n in ni.GetList())
            {
                sum += Dfs(n, depth + 1);
            }

            return sum;
        }
    }

    public class NestedInteger
    {
        private Nullable<int> _value;
        private List<NestedInteger> _list;

        // Constructor initializes an empty nested list.
        public NestedInteger()
        {
            _value = null;
            _list = new List<NestedInteger>();
        }

        // Constructor initializes a single integer.
        public NestedInteger(int value)
        {
            _value = value;
        }

        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        public bool IsInteger() => _value.HasValue;

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        public int GetInteger() => _value.Value;

        // Set this NestedInteger to hold a single integer.
        public void SetInteger(int value)
        {
            _value = value;
        }

        // Set this NestedInteger to hold a nested list and adds a nested integer to it.
        public void Add(NestedInteger ni)
        {
            if (_list == null)
            {
                _list = new List<NestedInteger>();
            }

            _list.Add(ni);
        }

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        public IList<NestedInteger> GetList() => _list;
    }
}
