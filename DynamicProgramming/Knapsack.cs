using System;
using System.Collections.Generic;

namespace CodingChallenges.DynamicProgramming
{
    internal class KnapSack
    {
        /* Given a list of items with values and weights, as well as a max weight, 
         * find the maximum value you can generate from items where the sum of the weights is less than the max.
         */
        public class Item
        {
            public int Weight { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return $"(W={Weight})(V={Value})";
            }
        }

        public static int Evaluate(Item[] items, int maxWeight)
        {
            return KSInternal(items, maxWeight, items.Length - 1, new Dictionary<int, Dictionary<int, int>>());
        }

        private static int KSInternal(Item[] items, int maxWeight, int index, Dictionary<int, Dictionary<int, int>> memoization)
        {
            if (maxWeight == 0 || index < 0)
            {
                return 0;
            }

            if (items[index].Weight > maxWeight)
            {
                return KSInternal(items, maxWeight, index - 1, memoization);
            }

            if (memoization.ContainsKey(maxWeight) && memoization[maxWeight].ContainsKey(index))
            {
                return memoization[maxWeight][index];
            }

            var item = items[index];
            var withItem = item.Value + KSInternal(items, maxWeight - item.Weight, index - 1, memoization);
            var withoutItem = KSInternal(items, maxWeight, index - 1, memoization);
            var maxValue = Math.Max(withItem, withoutItem);
            if (!memoization.ContainsKey(maxWeight))
            {
                memoization.Add(maxWeight, new Dictionary<int, int>());
            }
            memoization[maxWeight].Add(index, maxValue);
            return maxValue;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Knapsack Sample ***");
            var items = new Item[] {
                new Item { Weight = 1, Value = 6 },
                new Item { Weight = 2, Value = 10 },
                new Item { Weight = 3, Value = 12 } };

            var maxWeight = 5;
            var maxValue = Evaluate(items, maxWeight);
            Console.WriteLine($"Items are { string.Join<Item>(", ", items) }");            
            Console.WriteLine($"Max value = {maxValue} for Max weight {maxWeight}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
