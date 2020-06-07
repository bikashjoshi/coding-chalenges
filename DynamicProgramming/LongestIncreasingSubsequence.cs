using System;
using System.Collections.Generic;

namespace CodingChallenges.DynamicProgramming
{
    internal class LongestIncreasingSubsequence
    {
        /*
         * https://leetcode.com/problems/longest-increasing-subsequence/
         * Given an unsorted array of integers, find the length of longest increasing subsequence.
         */

        public static IEnumerable<int> LengthOfLongestIncreasingSubsequence(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return new List<int>();
            }

            var dp = new List<int>[nums.Length];
            for (var i = 0; i < nums.Length; i++)
            {
                dp[i] = new List<int> { nums[i] };
            }

            var maxIndex = 0;
            var maxCount = 1;
            for (var i = 1; i < nums.Length; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j] && dp[i].Count < dp[j].Count + 1)
                    {
                        dp[i] = new List<int>(dp[j].Count + 1);
                        dp[i].AddRange(dp[j]);
                        dp[i].Add(nums[i]);
                        if (maxCount < dp[i].Count)
                        {
                            maxCount = dp[i].Count;
                            maxIndex = i;
                        }
                    }

                }
            }

            return dp[maxIndex];
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Longest Increasing Subsequence ***");
            /*
             * Input: [10,9,2,5,3,7,101,18]
               Output: 4 
               Explanation: The longest increasing subsequence is [2,3,7,101], therefore the length is 4. 
             */

            var input = new int[] { 10, 9, 2, 5, 3, 7, 101, 18 };          
            var output = LengthOfLongestIncreasingSubsequence(input);
            
            Console.WriteLine($"Input Array: {string.Join(",", input )}.");
            Console.WriteLine($"The longest increasing subsequence is: {string.Join(",", output)}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
