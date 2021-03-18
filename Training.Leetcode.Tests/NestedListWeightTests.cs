using System.Collections.Generic;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class NestedListWeightTests
    {
        [Theory]
        [InlineData(10, new object[] { new object[] { 1, 1 }, 2, new object[] { 1, 1 } })]
        [InlineData(27, new object[] { 1, new object[] { 4, new object[] { 6 } } })]
        [InlineData(0, new object[0])]
        public void NestedListWeightTest(int expected, object[] items)
        {
            var list = new List<NestedInteger>();
            foreach (var item in items)
            {
                list.Add(Dfs(item, new NestedInteger()));
            }
            Assert.Equal(expected, NestedListWeightSum.DepthSum(list));
        }

        private NestedInteger Dfs(object item, NestedInteger current)
        {
            if (item is int)
            {
                current.SetInteger((int)item);
                return current;
            }
            foreach (var obj in (object[])item)
            {
                current.Add(Dfs(obj, new NestedInteger()));
            }
            return current;
        }
    }
}
