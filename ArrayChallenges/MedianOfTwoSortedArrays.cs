using System;

namespace CodingChallenges.ArrayChallenges
{
    internal class MedianOfTwoSortedArrays
    {
        /* 
         * https://leetcode.com/problems/median-of-two-sorted-arrays/         
         */

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var length = nums1.Length + nums2.Length;
            var isEven = length % 2 == 0;

            int i = 0, j = 0, k = 0;
            int x = 0, y = 0;

            int midPointIndex = length / 2;

            Action<int, int[]> incrementAction = (index, array) => {
                if (!isEven && k == midPointIndex)
                {
                    x = array[index];
                }

                if (isEven && k == midPointIndex - 1)
                {
                    x = array[index];
                }

                if (isEven && k == midPointIndex)
                {
                    y = array[index];
                }
            };

            while (i < nums1.Length && j < nums2.Length && k <= midPointIndex)
            {
                if (nums1[i] < nums2[j])
                {
                    incrementAction(i, nums1);
                    i++;
                    k++;
                }
                else
                {
                    incrementAction(j, nums2);
                    j++;
                    k++;
                }
            }

            if (k <= midPointIndex)
            {
                while (i < nums1.Length && k <= midPointIndex)
                {
                    incrementAction(i, nums1);
                    i++;
                    k++;
                }

                while (j < nums2.Length && k <= midPointIndex)
                {
                    incrementAction(j, nums2);
                    j++;
                    k++;
                }
            }

            return isEven ? (double)(x + y) / 2 : x;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Median of Two Sorted Arrays ***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /*
            * nums1 = [1, 2]
              nums2 = [3, 4]
              The median is (2 + 3)/2 = 2.5
            */

            var nums1 = new int[] { 1, 2 };
            var nums2 = new int[] { 3, 4 };
            var median = FindMedianSortedArrays(nums1, nums2);
            Console.WriteLine($"Median of [{string.Join(",", nums1)}] and [{string.Join(",", nums2)}] is {median}.");
        }

        private static void RunSample2()
        {
            /*
             * nums1 = [1, 3]
               nums2 = [2]
               The median is 2.0
             */
            var nums1 = new int[] { 1, 3 };
            var nums2 = new int[] { 2 };
            var median = FindMedianSortedArrays(nums1, nums2);
            Console.WriteLine($"Median of [{string.Join(",", nums1)}] and [{string.Join(",", nums2)}] is {median}.");
        }
    }   
}
