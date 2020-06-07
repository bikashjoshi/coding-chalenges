using System;
using System.Collections.Generic;

namespace CodingChallenges.ArrayChallenges
{
    internal class ProductExceptSelf
    {
        /* 
         * https://leetcode.com/problems/product-of-array-except-self/
         * Note: Solve it without division and in O(n).
         */

        public static int[] GetProductExceptSelf(int[] nums)
        {
            var prefixes = new int[nums.Length];
            var suffixes = new int[nums.Length];
            for (var i = 0; i < nums.Length; i++)
            {
                var j = nums.Length - i - 1;
                var prefix = i != 0 ? prefixes[i - 1] * nums[i - 1] : 1;
                var suffix = j != nums.Length - 1 ? suffixes[j + 1] * nums[j + 1] : 1;
                prefixes[i] = prefix;
                suffixes[j] = suffix;
            }

            var product = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                product.Add(prefixes[i] * suffixes[i]);
            }

            return product.ToArray();
        }

        public static void RunSample()
        {
            /*
             * Input:  [1,2,3,4]
               Output: [24,12,8,6]
             * 
             */

            var input = new int[] { 1, 2, 3, 4 };
            var output = GetProductExceptSelf(input);
            ConsoleHelper.WriteGreen("*** Product Except Self without using division. ***");
            Console.WriteLine($"Input Array: {string.Join(",", input)}.");
            Console.WriteLine($"Product Except self is: {string.Join(",", output)}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
