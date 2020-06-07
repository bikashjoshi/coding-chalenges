using System;

namespace CodingChallenges.HeapChallenges
{
    internal class RunningMedian
    {
        /*
         * https://www.hackerrank.com/challenges/ctci-find-the-running-median/problem
         */

        public static double[] GetMedians(int[] array)
        {
            var lowers = new Heap<int>(array.Length);
            var highers = new Heap<int>(array.Length, true);
            var medians = new System.Collections.Generic.List<double>();
            for (int i = 0; i < array.Length; i++)
            {
                AddNumber(array[i], lowers, highers);
                Rebalance(lowers, highers);
                var median = GetMedian(lowers, highers);
                ConsoleHelper.WriteYellow($"Median is {median}");                
                medians.Add(median);
            }

            while(!lowers.IsEmpty)
            {
                ConsoleHelper.WriteYellow($"Deleting lower: {lowers.Delete()}");
            }

            while(!highers.IsEmpty)
            {
                ConsoleHelper.WriteYellow($"Deleting higher:{highers.Delete()}");
            }

            return medians.ToArray();
        }
      
        private static void AddNumber(int num, Heap<int> lowers, Heap<int> highers)
        {
            if (lowers.IsEmpty || num < lowers.Peek())
            {
                ConsoleHelper.WriteYellow($"Added {num} in lower Heap.");
                lowers.Insert(num);
            }
            else
            {
                ConsoleHelper.WriteYellow($"Added {num} in higher Heap.");
                highers.Insert(num);
            }
        }

        private static void Rebalance(Heap<int> lowers, Heap<int> highers)
        {
            var condition = lowers.Size > highers.Size;
            var biggerHeap = condition ? lowers : highers;
            var smallerHeap = condition ? highers : lowers;

            if (biggerHeap.Size - smallerHeap.Size >= 2)
            {
                var biggerSize = biggerHeap.Size;
                var smallerSize = smallerHeap.Size;
                var bigger = condition ? "Lower" : "Higher";
                var item = biggerHeap.Delete();
                Console.WriteLine($"{bigger} is Bigger. Bigger heap size is {biggerSize}, and smaller heap size is {smallerSize}. Moving {item} from bigger to smaller.");
                smallerHeap.Insert(item);
            }
        }

        private static double GetMedian(Heap<int> lowers, Heap<int> highers)
        {
            var condition = lowers.Size > highers.Size;
            var biggerHeap = condition ? lowers : highers;
            var smallerHeap = condition ? highers : lowers;
            if (biggerHeap.Size == smallerHeap.Size)
            {
                return ((double)biggerHeap.Peek() + (double)smallerHeap.Peek()) / 2;
            }

            return biggerHeap.Peek();
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Running Median ***");
            int n = 10;
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine($"Input array: { string.Join(" ", a) }");
            var medians = GetMedians(a);
            Console.WriteLine($"Running medians are {string.Join(", ", medians)}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
