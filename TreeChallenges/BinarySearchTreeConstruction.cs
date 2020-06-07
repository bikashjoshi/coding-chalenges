using System;

namespace CodingChallenges.TreeChallenges
{
    internal class BinarySearchTreeConstruction
    {
        /*
         * https://leetcode.com/problems/construct-binary-search-tree-from-preorder-traversal/
         */

        public static TreeNode BstFromPreorder(int[] preorder)
        {
            if (preorder.Length == 0)
            {
                return null;
            }

            return BstFromPreorder(preorder, 0, preorder.Length - 1);
        }

        private static TreeNode BstFromPreorder(int[] preorder, int startIndex, int endIndex)
        {
            if (preorder.Length == 0)
            {
                return null;
            }

            var root = preorder[startIndex];
            var rootNode = new TreeNode(root);

            if (endIndex == startIndex)
            {
                return rootNode;
            }

            var highIndex = 0;
            for (var i = startIndex + 1; i <= endIndex; i++)
            {
                if (preorder[i] > root)
                {
                    highIndex = i;
                    break;
                }
            }

            var leftArrayEndIndex = highIndex > 0 ? highIndex - 1 : endIndex;
            var leftArrayStartIndex = startIndex + 1 < highIndex || highIndex == 0 ? startIndex + 1 : -1;
            if (leftArrayStartIndex > 0 && leftArrayEndIndex > 0)
            {
                rootNode.left = BstFromPreorder(preorder, leftArrayStartIndex, leftArrayEndIndex);
            }

            if (highIndex > 0)
            {
                rootNode.right = BstFromPreorder(preorder, highIndex, endIndex);
            }


            return rootNode;
        }

        public static void RunSample()
        {
            /*
             * inorder = [8,5,1,7,10,12]               
             */
            ConsoleHelper.WriteGreen("*** Binary Search Tree Construction ***");
            var preOrder = new int[] { 8, 5, 1, 7, 10, 12 };            

            Console.WriteLine($"In Order: {string.Join(", ", preOrder)}");          

            var root = BstFromPreorder(preOrder);
            Console.WriteLine($"Reconstructed Tree's traveral In Order: {string.Join(", ", TreeNode.InOrder(root))}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }  
}
