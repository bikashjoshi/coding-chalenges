using System;

namespace CodingChallenges.ArrayChallenges
{
    internal class BinarySearchInRotatedArray
    {
        public static int FindMinIndex(int[] nums)
        {
            /* https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/ */
            var low = 0;
            var high = nums.Length - 1;
            while (high > low)
            {
                var mid = low + (high - low) / 2;
                if (nums[mid] < nums[high])
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return low;
        }

        public static int FindIndex(int[] nums, int target)
        {
            /* https://leetcode.com/problems/search-in-rotated-sorted-array/
             * A sorted array of integers has been 
             * rotated an unknown number of times.
             *  Input: [13, 18, 25, 2, 8, 10, 11, 12].
             *  Find the index of 8.
            */

            if (nums.Length == 0)
            {
                return -1;
            }

            if (nums.Length == 1)
            {
                return nums[0] == target ? 0 : -1;
            }

            var low = FindMinIndex(nums);
            var high = low == 0 ? nums.Length - 1 : low - 1;
            var dist = nums.Length / 2;
            var executedOneCounter = 0;

            while (true)
            {
                if (dist == 0 || executedOneCounter >= 2)
                {
                    return -1;
                }

                if (dist == 1)
                {
                    executedOneCounter++;
                }

                var midPointIndex = GetMidPoint(low, high, nums.Length);
                var midPointValue = nums[midPointIndex];

                if (midPointValue == target)
                {
                    return midPointIndex;
                }

                if (midPointValue < target)
                {
                    low = midPointIndex + 1;
                }
                if (midPointValue > target)
                {
                    high = midPointIndex == 0 ? nums.Length - 1 : midPointIndex - 1;
                }

                dist = (int)Math.Ceiling((double)dist / 2);
            }
        }

        private static int GetMidPoint(int low, int high, int length)
        {
            if (high < low)
            {
                high += length;
            }

            var midPoint = (low + high) / 2;

            return midPoint % length;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Binary Search In Rotated Array ***");
            var array = new int[] { 6, 7, 1, 2, 3, 4, 5 };
            Console.WriteLine($"{string.Join(", ", array)}");
            var result = FindIndex(array, 2);
            Console.WriteLine($"Index of 2 is: {result}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
