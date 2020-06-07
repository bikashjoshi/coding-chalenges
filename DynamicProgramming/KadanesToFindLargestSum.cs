using System;

namespace CodingChallenges.DynamicProgramming
{
    internal class KadanesToFindLargestSum
    {
        /*
         * https://leetcode.com/problems/maximum-subarray/
         */

        public class Result
        {
            public int Sum { get; set; }

            public int StartIndex { get; set; }

            public int EndIndex { get; set; }
        }

        public static Result GetLargestSum(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            int maxSoFar = input[0];
            int maxEndingHere = 0;
            int startIndex = 0, endIndex = 0, trackingIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                maxEndingHere += input[i];
                if (maxSoFar < maxEndingHere)
                {
                    maxSoFar = maxEndingHere;
                    startIndex = trackingIndex;
                    endIndex = i;
                }

                if (maxEndingHere < 0)
                {
                    maxEndingHere = 0;
                    trackingIndex = i + 1;
                }
            }

            return new Result
            {
                Sum = maxSoFar,
                StartIndex = startIndex,
                EndIndex = endIndex
            };
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Kadane's Algorithm to find the contiguous subarray which has the largest sum. ***");

            var input = new int[] { 4, -3, -2, 2, 3, 1, -2, -3, 4, 2, -6, -3, -1, 3, 1, 2 };
            Console.WriteLine("Input: " + string.Join(", ", input));
            var result = GetLargestSum(input);
            Console.WriteLine("Largest Sum: {0}, Start Index: {1}, End Index: {2}", result.Sum, result.StartIndex, result.EndIndex);
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
