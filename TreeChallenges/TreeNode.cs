using System.Collections.Generic;

namespace CodingChallenges.TreeChallenges
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public static IEnumerable<int> InOrder(TreeNode node)
        {
            if (node != null)
            {
                foreach (var item in InOrder(node.left))
                {
                    yield return item;
                }

                yield return node.val;

                foreach (var item in InOrder(node.right))
                {
                    yield return item;
                }
            }
        }
    }
}
