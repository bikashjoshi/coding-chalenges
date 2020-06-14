using System.Collections.Generic;

namespace CodingChallenges.DynamicProgramming
{
    internal class PaintingHouses
    {
        /* https://leetcode.com/problems/paint-house/description/
         * There are a row of n houses, each house can be painted with one of the three colors: 
         * red, blue or green. The cost of painting each house with a certain color is different. 
         * You have to paint all the houses such that no two adjacent houses have the same color.
         */

        public static int FindMinimum(int[][] costs)
        {
            var totalHouses = costs.Length;
            var totalColors = costs[0].Length;
            var mincosts = new int[totalColors];

            for(var color = 0; color < totalColors; color++)
            {
                mincosts[color] = costs[0][color];
            }

            for(var houseIndex = 1; houseIndex < totalHouses; houseIndex++)
            {
                var current = new List<int>(totalColors);
                for(var color = 0; color < totalColors; color++)
                {
                    var neighborsMin = GetNeighborsMin(mincosts, color);
                    var currentHouseColor = costs[houseIndex][color] + neighborsMin;
                    current.Add(currentHouseColor);
                }

                for(var color = 0; color < current.Count; color++)
                {
                    mincosts[color] = current[color];
                }
            }

            return GetNeighborsMin(mincosts);
        }

        private static int GetNeighborsMin(int[] neighborColors, int? skipColor = null)
        {
            var neighborHousesMinCost = int.MaxValue;            
            for (var neighborColor = 0; neighborColor < neighborColors.Length; neighborColor++)
            {
                if (skipColor.HasValue && neighborColor == skipColor.Value)
                {
                    continue;
                }

                if (neighborHousesMinCost > neighborColors[neighborColor])
                {
                    neighborHousesMinCost = neighborColors[neighborColor];                    
                }
            }

            return neighborHousesMinCost;
        }

        public static void RunSample()
        {
            RunSample1();
            RunSample2();
        }

        private static void RunSample1()
        {
            int[][] costs = new int[][]{
                new int[] { 17, 2, 1 },
                new int[] { 16, 16, 1 },
                new int[] { 14, 3, 19 },
                new int[] { 3, 1, 8 }
            };

            FindMinimumAndPrint(costs);
        }

        private static void RunSample2()
        {
            int[][] costs = new int[][]{
                new int[] { 5, 8, 6 },
                new int[] { 19, 14, 13 },
                new int[] { 7, 5, 12 },
                new int[] { 14, 15, 17 },
                new int[] { 3, 20, 10 }
            };

            FindMinimumAndPrint(costs);
        }

        private static void FindMinimumAndPrint(int[][] costs)
        {
            PrintCost("Costs of Painting House", costs);
            int minCost = FindMinimum(costs);            
            ConsoleHelper.WritePink($"Minimum cost is {minCost}");
        }

        private static void PrintCost(string message, int[][] costs)
        {
            ConsoleHelper.WriteBlue(message);
            foreach(var row in costs)
            {
                ConsoleHelper.WriteYellow(string.Join(", ", row));
            }
        }
    }
}
