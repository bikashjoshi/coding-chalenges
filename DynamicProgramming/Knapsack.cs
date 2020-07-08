using System;
using System.Collections.Generic;

namespace CodingChallenges.DynamicProgramming
{
    internal class KnapSack
    {
        /* https://www.hackerrank.com/contests/srin-aadc03/challenges/classic-01-knapsack/problem
         *Given a list of items with values and weights, as well as a max weight, 
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

        private static int EvaluateBottomup(Item[] items, int maxWeight)
        {
            var dp = new int[items.Length + 1][];
            for (var i = 0; i < items.Length + 1; i++)
            {
                dp[i] = new int[maxWeight + 1];
                for (var j = 0; j < maxWeight + 1; j++)
                {
                    dp[i][j] = 0;
                }
            }

            for (var i = 0; i < items.Length; i++)
            {
                for (var weight = 1; weight <= maxWeight; weight++)
                {
                    var currentItem = items[i];
                    int currentWeight = currentItem.Weight;
                    var without = dp[i][weight];
                    if (currentWeight <= weight)
                    {
                        var with = currentItem.Value + dp[i][weight - currentWeight];                        
                        dp[i + 1][weight] = Math.Max(with, without);
                    }
                    else
                    {
                        dp[i + 1][weight] = without;
                    }
                }
            }

            return dp[items.Length][maxWeight];
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Knapsack Sample ***");
            /*
             *  1 8
                2 4
                3 0
                2 5
                2 3
             */
            var items = new Item[] {
                new Item { Weight = 1, Value = 8 },
                new Item { Weight = 2, Value = 4 },
                new Item { Weight = 3, Value = 0 },
                new Item { Weight = 2, Value = 5 },
                new Item { Weight = 2, Value = 3 }
            };

            // Array.Sort(items, (x, y) => x.Weight == y.Weight ? x.Value.CompareTo(y.Value) : x.Weight.CompareTo(y.Weight));
            var maxWeight = 4;
            var maxValue = EvaluateBottomup(items, maxWeight);
            Console.WriteLine($"Items are { string.Join<Item>(", ", items) }");            
            Console.WriteLine($"Max value = {maxValue} for Max weight {maxWeight}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
