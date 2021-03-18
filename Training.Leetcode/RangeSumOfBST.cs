namespace Training.Leetcode
{
    public class RangeSumOfBST
    {
        public static int RangeSumBST(TreeNode root, int L, int R)
        {
            if (root == null)
            {
                return 0;
            }

            var sum = L <= root.val && root.val <= R
                ? root.val
                : 0;

            if (L < root.val)
            {
                sum += RangeSumBST(root.left, L, R);
            }
            if (root.val < R)
            {
                sum += RangeSumBST(root.right, L, R);
            }
            return sum;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) => val = x;
        }
    }
}
