using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class Dijkstra
    {
        /* https://leetcode.com/problems/minimum-path-sum/ */
       
        public class Coordinate
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }

        public class WeightsAndPath
        {
            public WeightsAndPath()
            {
                Weight = int.MaxValue;
            }

            public void AssignPath(IEnumerable<Coordinate> soFar, int row, int col)
            {
                Path = new List<Coordinate>(soFar) { new Coordinate { Row = row, Col = col } };
            }

            public int Weight { get; set; }

            public List<Coordinate> Path { get;  set; }
        }

        public static WeightsAndPath MinPathSum(int[][] grid)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;

            var weights = new WeightsAndPath[rows][];            
            for (var i = 0; i < rows; i++)
            {
                weights[i] = new WeightsAndPath[cols];
                for (var j = 0; j < cols; j++)
                {
                    weights[i][j] = new WeightsAndPath();
                }
            }

            weights[0][0].Weight = grid[0][0];
            weights[0][0].AssignPath(Enumerable.Empty<Coordinate>(), 0, 0);
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {                  
                    var currentWeightAndPath = weights[row][col];
                    var rightRow = row;
                    var rightCol = col + 1;
                    
                    if (rightCol < cols)
                    {
                        var weight = currentWeightAndPath.Weight + grid[rightRow][rightCol];
                        if (weights[rightRow][rightCol].Weight > weight)
                        {
                            weights[rightRow][rightCol].Weight = weight;
                            weights[rightRow][rightCol].AssignPath(currentWeightAndPath.Path, rightRow, rightCol);
                        }
                    }

                    var bottomRow = row + 1;
                    var bottomCol = col;

                    if (bottomRow < rows)
                    {
                        var weight = currentWeightAndPath.Weight + grid[bottomRow][bottomCol];
                        if (weights[bottomRow][bottomCol].Weight > weight)
                        {
                            weights[bottomRow][bottomCol].Weight = weight;
                            weights[bottomRow][bottomCol].AssignPath(currentWeightAndPath.Path, bottomRow, bottomCol);
                        }
                    }
                }
            }

            /* // Intermediate Path and Weight display
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var weightAndPath = weights[row][col];
                    Console.WriteLine($"({row}, {col}) takes {weightAndPath.Weight} weight with path : { string.Join(" => ", weightAndPath.Path.Select(x => $"({x.Row}, {x.Col})[{grid[x.Row][x.Col]}]")) }");
                }
            }*/

            return weights[rows - 1][cols - 1];
        }

        public static void RunSample()
        {
            RunSample1();
        }     

        private static void RunSample1()
        {
            ConsoleHelper.WriteGreen("*** Running Min Path Sum using Dijkstra ***");
            var grid = new[]{
                                new int[] { 7,1,3,5,8,9,9,2,1,9,0,8,3,1,6,6,9,5 },
                                new int[] { 9,5,9,4,0,4,8,8,9,5,7,3,6,6,6,9,1,6 },
                                new int[] { 8,2,9,1,3,1,9,7,2,5,3,1,2,4,8,2,8,8 },
                                new int[] { 6,7,9,8,4,8,3,0,4,0,9,6,6,0,0,5,1,4 },
                                new int[] { 7,1,3,1,8,8,3,1,2,1,5,0,2,1,9,1,1,4 },
                                new int[] { 9,5,4,3,5,6,1,3,6,4,9,7,0,8,0,3,9,9 },
                                new int[] { 1,4,2,5,8,7,7,0,0,7,1,2,1,2,7,7,7,4 },
                                new int[] { 3,9,7,9,5,8,9,5,6,9,8,8,0,1,4,2,8,2 },
                                new int[] { 1,5,2,2,2,5,6,3,9,3,1,7,9,6,8,6,8,3 },
                                new int[] { 5,7,8,3,8,8,3,9,9,8,1,9,2,5,4,7,7,7 },
                                new int[] { 2,3,2,4,8,5,1,7,2,9,5,2,4,2,9,2,8,7 },
                                new int[] { 0,1,6,1,1,0,0,6,5,4,3,4,3,7,9,6,1,9 }
                                };

            foreach(var row in grid)
            {
                Console.WriteLine(string.Join(", ", row));
            }

            var result = MinPathSum(grid);
            Console.WriteLine($"Minimum sum is {result.Weight} and path is:");
            Console.WriteLine(string.Join(" => ", result.Path.Select(x => $"[{x.Row}][{x.Col}]")));
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
