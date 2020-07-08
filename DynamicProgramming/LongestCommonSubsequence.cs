using System;
using System.Text;

namespace CodingChallenges.DynamicProgramming
{
    internal class LongestCommonSubsequence
    {
        /* https://leetcode.com/problems/longest-common-subsequence/
         */

        public static string LongestCommonSubsequenceWithDP(string text1, string text2)
        {
            var m = text1.Length;
            var n = text2.Length;
            var dp = new StringBuilder[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new StringBuilder[n];
            }
            
            var longest = LongestCommonSubsequenceWithDP(text1, text2, 0, 0, dp);            
            return longest.ToString();
        }

        private static StringBuilder LongestCommonSubsequenceWithDP(string s1, string s2, int i, int j, StringBuilder[][] dp)
        {
            if (i >= s1.Length || j >= s2.Length)
            {               
                return null;
            }
            if (dp[i][j] != null)
            {
                return dp[i][j];
            }

            if (s1[i] == s2[j])
            {   
                if (dp[i][j] == null)
                {
                    dp[i][j] = new StringBuilder();
                }

                dp[i][j].Append(s1[i]);
                return dp[i][j].Append(LongestCommonSubsequenceWithDP(s1, s2, i + 1, j + 1, dp));
            }
            else
            {
                var result1 = LongestCommonSubsequenceWithDP(s1, s2, i + 1, j, dp);
                var result2 = LongestCommonSubsequenceWithDP(s1, s2, i, j + 1, dp);
                if (dp[i][j] == null)
                {
                    dp[i][j] = new StringBuilder();
                }

                var longest = result1 != null ? result1 : new StringBuilder();
                longest = result2 != null && result2.Length > longest.Length ? result2 : longest;
                return dp[i][j].Append(longest);
            }
        }

        private static StringBuilder LCSBottomup(string text1, string text2)
        {
            var m = text1.Length;
            var n = text2.Length;
            var dp = new StringBuilder[m + 1][];
            for (int i = 0; i <= m; i++)
            {
                dp[i] = new StringBuilder[n + 1];
                for (int j = 0; j <= n; j++)
                {
                    dp[i][j] = new StringBuilder();                                      
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (text1[i] == text2[j])
                    {
                        dp[i + 1][j + 1].Append(dp[i][j]);
                        dp[i + 1][j + 1].Append(text1[i]);
                    }
                    else
                    {
                        var max = dp[i][j + 1].Length > dp[i + 1][j].Length ? dp[i][j + 1] : dp[i + 1][j];
                        dp[i + 1][j + 1].Append(max);
                    }
                }
            }


            return dp[m][n];
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Longest Common Sequence ***");
            var text1 = "szulspmhwpazoxijwbq";
            var text2 = "mhunuzqrkzsnidwbun";
            var longest = LongestCommonSubsequenceWithDP(text1, text2);
            var longest2 = LCSBottomup(text1, text2);
            Console.WriteLine($"The longest common sequence of {text1} and {text2} is {longest} with length {longest.Length}.");
            Console.WriteLine($"With Bottom up, the longest common sequence of {text1} and {text2} is {longest2} with length {longest2.Length}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
