using Xunit;
using TreeNode = Training.Leetcode.RangeSumOfBST.TreeNode;
namespace Training.Leetcode.Tests
{
    public class RangeSumOfBSTTests
    {
        [Theory]
        [InlineData(32, 7, 15, new object[] { 10, 5, 15, 3, 7, null, 18 })]
        [InlineData(0, 0, 0, new object[0])]
        public void RangeSumOfBSTTest(int expected, int left, int right, object[] nums)
        {
            var root = BuildBST(nums, 0);
            Assert.Equal(expected, RangeSumOfBST.RangeSumBST(root, left, right));
        }
        private TreeNode BuildBST(object[] nums, int i)
        {
            if (i > nums.Length - 1 || nums[i] == null)
                return null;

            var root = new TreeNode((int)nums[i]);
            root.left = BuildBST(nums, 2 * i + 1);
            root.right = BuildBST(nums, 2 * i + 2);
            return root;
        }
    }
}
