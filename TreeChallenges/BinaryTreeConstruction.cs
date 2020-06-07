using System;

namespace CodingChallenges.TreeChallenges
{
    internal class BinaryTreeConstruction
    {
        /*
         * https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/
         */

        public static TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            if (inorder.Length == 0)
            {
                return null;
            }

            var root = postorder[postorder.Length - 1];
            var rootNode = new TreeNode(root);

            if (inorder.Length == 1 || postorder.Length == 1)
            {
                return rootNode;
            }

            var rootIndex = Array.IndexOf(inorder, root);
            var leftArrayInOrder = new int[rootIndex];
            Array.Copy(inorder, 0, leftArrayInOrder, 0, leftArrayInOrder.Length);

            var rightArrayInOrder = new int[inorder.Length - rootIndex - 1];
            Array.Copy(inorder, rootIndex + 1, rightArrayInOrder, 0, rightArrayInOrder.Length);

            var leftArrayPostOrder = new int[leftArrayInOrder.Length];
            Array.Copy(postorder, 0, leftArrayPostOrder, 0, leftArrayPostOrder.Length);

            var rightArrayPostOrder = new int[rightArrayInOrder.Length];
            Array.Copy(postorder, leftArrayPostOrder.Length, rightArrayPostOrder, 0, rightArrayPostOrder.Length);

            rootNode.left = BuildTree(leftArrayInOrder, leftArrayPostOrder);
            rootNode.right = BuildTree(rightArrayInOrder, rightArrayPostOrder);

            return rootNode;
        }

        public static void RunSample()
        {
            /*
             * inorder = [9,3,15,20,7]
               postorder = [9,15,7,20,3]                                
             */
            ConsoleHelper.WriteGreen("*** Binary Tree Construction ***");
            var inorder = new int[] { 9, 3, 15, 20, 7 };
            var postorder = new int[] { 9, 15, 7, 20, 3 };

            Console.WriteLine($"In Order: {string.Join(", ", inorder)}");
            Console.WriteLine($"Post Order: {string.Join(", ", postorder)}");

            var root = BuildTree(inorder, postorder);                        
            Console.WriteLine($"Reconstructed Tree's traveral In Order: {string.Join(", ", TreeNode.InOrder(root))}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
