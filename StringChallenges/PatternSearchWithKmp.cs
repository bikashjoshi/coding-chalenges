using System;

namespace CodingChallenges.StringChallenges
{
    internal class PatternSearchWithKmp
    {
        /*
         * https://leetcode.com/problems/implement-strstr/
         */

        private static int[] GetNextArray(string pattern)
        {
            var next = new int[pattern.Length];
            var j = 0;
            var i = 1;
            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[j])
                {
                    next[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j == 0)
                {
                    next[i] = 0;
                    i++;
                }
                else
                {
                    j = next[j - 1];
                }
            }

            return next;
        }

        public static int FindIndex(string haystack, string needle)
        {
            if (needle == string.Empty)
            {
                return 0;
            }

            var next = GetNextArray(needle);
            var i = 0;
            var j = 0;
            while (i < haystack.Length && j < needle.Length)
            {
                if (haystack[i] == needle[j])
                {
                    i++;
                    j++;
                }
                else if (j == 0)
                {
                    i++;
                }
                else
                {
                    j = next[j - 1];
                }

                if (j == needle.Length)
                {
                    return i - needle.Length;
                }
            }

            return -1;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** KMP Algorithm to find the pattern matching in given text. ***");
            var text = "mississippi";
            var pattern = "issip";

            var result = FindIndex(text, pattern);
            Console.WriteLine($"Index for pattern {pattern} in the text {text} is {result}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
