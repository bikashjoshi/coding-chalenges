using System;

namespace CodingChallenges.TreeChallenges
{   
    internal class BinarySearchTreeBalanceDetector
    {      
        /*
         * https://leetcode.com/problems/balanced-binary-tree/
         */

        public static bool IsBalanced(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            var leftHeight = GetHeight(root.left);
            var rightHeight = GetHeight(root.right);

            return Math.Abs(leftHeight - rightHeight) <= 1
                && IsBalanced(root.left)
                && IsBalanced(root.right);
        }

        private static int GetHeight(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            return 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Balanced Tree Detector ***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /*
             * Input [3,9,20,null,null,15,7]
             *      3
                   / \
                  9  20
                    /  \
                   15   7
              Expected: true
             */
            var root = new TreeNode(3)
            {
                left = new TreeNode(9),
                right = new TreeNode(20)
                {
                    left = new TreeNode(15),
                    right = new TreeNode(7)
                }
            };           

            Console.WriteLine($"Tree [3,9,20,null,null,15,7] is balanced: {IsBalanced(root)}");
        }

        private static void RunSample2()
        {
            /*
             * Input [1,2,2,3,3,null,null,4,4]
             *         1
                      / \
                     2   2
                    / \
                   3   3
                  / \
                 4   4
                Expected: false
             */
            var root = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(3)
                    {
                        left = new TreeNode(4),
                        right = new TreeNode(4)
                    },
                    right = new TreeNode(3)
                },
                right = new TreeNode(2)
            };

            Console.WriteLine($"Tree [1,2,2,3,3,null,null,4,4] is balanced: {IsBalanced(root)}");
        }
    }
}
