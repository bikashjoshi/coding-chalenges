using System;
using System.Collections.Generic;

namespace CodingChallenges.DynamicProgramming
{
    internal class Staircase
    {
        /* 
         * https://leetcode.com/problems/climbing-stairs/
         */
     
        public static int BottomUpStairCaseWays(int n)
        {
            var cache = new List<int>();
            cache.Insert(0, 1);
            cache.Insert(1, 1);

            for (var index = 2; index <= n; index++)
            {
                cache.Insert(index, cache[index - 1] + cache[index - 2]);
            }

            return cache[n];            
        }

        private static int MemoizationStaircase(int n)
        {
            var cache = new List<int>();
            cache.Insert(0, 1);
            cache.Insert(1, 1);
            return MemoizationStaircaseInternal(n, cache);
        }

        private static int MemoizationStaircaseInternal(int n, List<int> cache)
        {
            if (n < cache.Count) // cache already contains computation for f(n)
            {
                return cache[n]; // this is f(n)
            }

            if (n < 0)
            {
                return 0;
            }

            var sum = MemoizationStaircaseInternal(n - 1, cache) + MemoizationStaircaseInternal(n - 2, cache);
            cache.Insert(n, sum);
            return sum;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** DP Sample for calculating possible distinct ways can to climb to the stairs ***");
            var n = 10;                        
            Console.WriteLine($"Using Bottom up for n = {n}: {BottomUpStairCaseWays(n)}");
            Console.WriteLine($"Using Memoization for n = {n}: {MemoizationStaircase(n)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
