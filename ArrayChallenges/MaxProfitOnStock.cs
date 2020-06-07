using System;

namespace CodingChallenges.ArrayChallenges
{
    internal class MaxProfitOnStock
    {
        /* https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/ */

        public static int MaxProfit(int[] prices)
        {
            var maxProfit = 0;
            var buyPrice = int.MaxValue;
            var sellPrice = int.MinValue;
            var buyIndex = -1;
            var sellIndex = prices.Length;
            for (var i = 0; i < prices.Length; i++)
            {
                var price = prices[i];

                if (price <= buyPrice && i < sellIndex)
                {                   
                    buyPrice = price;
                    buyIndex = i;
                }

                if (price > sellPrice && i > buyIndex)
                {
                    sellPrice = price;
                    sellIndex = i;
                }

                if ((price < sellPrice || i == prices.Length - 1) && sellPrice > buyPrice)
                {
                    Console.WriteLine($"Buy at {buyPrice} on [{buyIndex}] and Sell at {sellPrice} on [{sellIndex}] to make {sellPrice - buyPrice} profit.");
                    maxProfit += sellPrice - buyPrice;
                    buyPrice = price;                    
                    buyIndex = i;
                    sellPrice = int.MinValue;
                    sellIndex = prices.Length;
                }                 
            }

            return maxProfit;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Running Max Profit On Stock Sample ***");
            var nums = new[] { 8, 6, 4, 3, 3, 2, 3, 5, 8, 3, 8, 2, 6 };
            Console.WriteLine($"Input array is {string.Join(", ", nums)}");
            var result = MaxProfit(nums);
            Console.WriteLine($"The maximum profit is {result}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
