namespace Training.Leetcode
{
    public class ConvertSortedArrayToBinarySearchTree
    {

        // this works as for my tests but does not passe the leetcode tests
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            var mid = nums.Length / 2;
            var root = new TreeNode(nums[mid]);
            BuildBST(root, 0, mid, nums);
            BuildBST(root, mid + 1, nums.Length, nums);
            return root;
        }

        private static void BuildBST(TreeNode current, int i, int top, int[] nums)
        {
            if (i == top)
            {
                return;
            }

            if (nums[i] <= current.val)
            {
                current.left = new TreeNode(nums[i]);
                current = current.left;
            }
            else
            {
                current.right = new TreeNode(nums[i]);
                current = current.right;
            }
            BuildBST(current, i + 1, top, nums);
        }

        // withoud seeing yet the solution
        public static TreeNode SortedArrayToBST2(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            var mid = nums.Length / 2;
            var root = new TreeNode(nums[mid]);
            var current = root;
            var i = -1;
            while (i < nums.Length - 1)
            {
                i++;
                if (i == mid)
                {
                    current = root;
                    continue;
                }

                if (nums[i] <= current.val)
                {
                    current.left = new TreeNode(nums[i]);
                    current = current.left;
                }
                else
                {
                    current.right = new TreeNode(nums[i]);
                    current = current.right;
                }
            }
            return root;
        }

        // without seeing to solution
        public static TreeNode SortedArrayToBST3(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            var mid = nums.Length / 2;
            var root = new TreeNode(nums[mid]);
            var current = root;
            var i = nums.Length - 1;
            var j = 2;
            while (j > 0)
            {
                while (i != mid && i >= 0)
                {
                    if (nums[i] <= current.val)
                    {
                        current.left = new TreeNode(nums[i]);
                        current = current.left;
                    }
                    else
                    {
                        current.right = new TreeNode(nums[i]);
                        current = current.right;
                    }
                    i--;
                }
                current = root;
                i--;
                j--;
            }
            return root;
        }

        // without seeing the solution
        public static TreeNode SortedArrayToBST4(int[] nums)
        {
            return BuildTree(0, nums.Length, nums);
        }

        private static TreeNode BuildTree(int low, int high, int[] nums)
        {
            if (low >= high)
            {
                return null;
            }

            var mid = low + ((high - low) / 2);
            var root = new TreeNode(nums[mid]);
            var left = BuildTree(low, mid, nums);
            var right = BuildTree(mid + 1, high, nums);
            root.left = left;
            root.right = right;
            return root;
        }

        // after seing solution
        public static TreeNode SortedArrayToBST5(int[] nums)
        {
            return BuildTree2(0, nums.Length - 1, nums);
        }

        private static TreeNode BuildTree2(int low, int high, int[] nums)
        {
            if (low > high)
            {
                return null;
            }

            var mid = (low + high) / 2;
            var root = new TreeNode(nums[mid])
            {
                left = BuildTree2(low, mid - 1, nums),
                right = BuildTree2(mid + 1, high, nums)
            };
            return root;
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
