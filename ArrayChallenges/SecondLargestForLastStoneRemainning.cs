using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.ArrayChallenges
{
    internal class SecondLargestForLastStoneRemainning
    {
        /* https://leetcode.com/problems/last-stone-weight/ */

        private class SearchItems
        {
            public int FirstLargest { get; set; }
            public int SecondLargest { get; set; }
            public int FirstIndex { get; set; }
            public int SecondIndex { get; set; }
        }

        public static int LastStoneWeight(int[] stones)
        {
            if (stones.Length == 0)
            {
                return 0;
            }

            var list = stones.ToList();
            while (list.Count >= 2)
            {
                var searchItems = FindFirstTwoLargest(list);
                if (searchItems.FirstLargest == searchItems.SecondLargest)
                {
                    var secondIndexToRemove = searchItems.SecondIndex < searchItems.FirstIndex
                        ? searchItems.SecondIndex : searchItems.SecondIndex - 1;
                    list.RemoveAt(searchItems.FirstIndex);
                    list.RemoveAt(secondIndexToRemove);
                }
                else
                {
                    list[searchItems.FirstIndex] = searchItems.FirstLargest - searchItems.SecondLargest;
                    list.RemoveAt(searchItems.SecondIndex);
                }
            }

            return list.Count == 0 ? 0 : list[0];

        }

        private static SearchItems FindFirstTwoLargest(List<int> stones)
        {
            var largest = int.MinValue;
            var secondLargest = int.MinValue;
            var firstIndex = -1;
            var secondIndex = -1;

            for (int i = 0; i < stones.Count; i++)
            {
                if (stones[i] > largest)
                {
                    secondIndex = firstIndex;
                    secondLargest = largest;
                    firstIndex = i;
                    largest = stones[i];
                }

                if (stones[i] > secondLargest && stones[i] < largest)
                {
                    secondIndex = i;
                    secondLargest = stones[i];
                }

                if (i != firstIndex && stones[i] == largest)
                {
                    secondIndex = i;
                    secondLargest = stones[i];
                }
            }

            return new SearchItems { FirstLargest = largest, SecondLargest = secondLargest, FirstIndex = firstIndex, SecondIndex = secondIndex };
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Second Largest Number for finding Last Stone remaining ***");
            var stones = new int[] { 4, 3, 4, 3, 2 };
            Console.WriteLine($"Input array is {string.Join(", ", stones)}");
            var result = LastStoneWeight(stones);
            Console.WriteLine($"The last remainning stone has weight {result}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
