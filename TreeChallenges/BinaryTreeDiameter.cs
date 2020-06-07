using System;

namespace CodingChallenges.TreeChallenges
{
    internal class BinaryTreeDiameter
    {
        /*
         * https://leetcode.com/problems/diameter-of-binary-tree/
         */

        public static int DiameterOfBinaryTree(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var leftHeight = GetHeight(root.left);
            var rightHeight = GetHeight(root.right);
            var leftDiameter = DiameterOfBinaryTree(root.left);
            var rightDiameter = DiameterOfBinaryTree(root.right);
            return Math.Max(leftHeight + rightHeight, Math.Max(leftDiameter, rightDiameter));
        }

        private static int GetHeight(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var leftHeight = 1 + GetHeight(root.left);
            var rightHeight = 1 + GetHeight(root.right);

            return Math.Max(leftHeight, rightHeight);
        }

        public static void RunSample()
        {
            /*
             *        1
                     / \
                    2   3
                   / \     
                  4   5    
                  Expected: 3
             */

            var root = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(4),
                    right = new TreeNode(5)
                },
                right = new TreeNode(3)
            };

            Console.WriteLine("*** Diameter of a Sample Tree ***");
            Console.WriteLine($"{DiameterOfBinaryTree(root)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
