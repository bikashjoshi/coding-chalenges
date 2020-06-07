using System;
using System.Linq;

namespace CodingChallenges.TreeChallenges
{
    internal class BinarySearchTreeBalanceOperation
    {
        /*
         * https://leetcode.com/problems/balance-a-binary-search-tree/
         */

        public static TreeNode BalanceBST(TreeNode root)
        {
            var inorderItems = TreeNode.InOrder(root).ToArray();            
            return BstFromInOrder(inorderItems.ToArray());
        }

        public static TreeNode BstFromInOrder(int[] inorder)
        {
            if (inorder.Length == 0)
            {
                return null;
            }

            var rootIndex = inorder.Length / 2;
            var root = inorder[rootIndex];
            var rootNode = new TreeNode(root);

            if (inorder.Length == 1)
            {
                return rootNode;
            }

            var leftArrayLength = rootIndex;
            var leftArray = new int[leftArrayLength];
            Array.Copy(inorder, 0, leftArray, 0, leftArray.Length);
            rootNode.left = BstFromInOrder(leftArray);

            var highIndex = rootIndex + 1;

            if (highIndex < inorder.Length)
            {
                var rightArray = new int[inorder.Length - highIndex];
                Array.Copy(inorder, highIndex, rightArray, 0, rightArray.Length);
                rootNode.right = BstFromInOrder(rightArray);
            }


            return rootNode;
        }

        public static void RunSample()
        {
            /*
             * Input: root = [1,null,2,null,3,null,4,null,null]
                Output: [2,1,3,null,null,null,4]
                Explanation: This is not the only correct answer, [3,1,4,null,2,null,null] is also correct.
             */

            var root = new TreeNode(1)
            {
                right = new TreeNode(2)
                {
                    right = new TreeNode(3)
                    {
                        right = new TreeNode(4)
                    }
                }
            };

            ConsoleHelper.WriteGreen("*** Binary Search Tree Balance ***");
            Console.WriteLine($"Input Tree is balance: {BinarySearchTreeBalanceDetector.IsBalanced(root)}");

            var result = BalanceBST(root);
            Console.WriteLine($"Input Tree is balance: {BinarySearchTreeBalanceDetector.IsBalanced(result)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
