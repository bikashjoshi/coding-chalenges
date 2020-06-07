using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class BellmanFord
    {
        public class BellmanFordResult
        {
            public double Weight;
            public List<int> Paths;
        }

        public class NegativeCycleResult : BellmanFordResult
        {

        }

        public static BellmanFordResult[] Solve(double[][] grid)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;
            var weights = new BellmanFordResult[rows];
            weights[0] = new BellmanFordResult { Weight = 0, Paths = new List<int> { 0 } };
            for (var i = 1; i < rows; i++)
            {
                weights[i] = new BellmanFordResult { Weight = double.MaxValue };
            }

            for (var k = 1; k <= rows - 1; k++)
            {
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (weights[j].Weight > weights[i].Weight + grid[i][j])
                        {
                            weights[j].Weight = weights[i].Weight + grid[i][j];
                            weights[j].Paths = new List<int>(weights[i].Paths) { j };
                        }
                    }
                }
            }

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (weights[j].Weight > weights[i].Weight + grid[i][j])
                    {
                        weights[j] = new NegativeCycleResult
                        {
                            Weight = weights[i].Weight + grid[i][j],
                            Paths = new List<int>(weights[i].Paths) { j }
                        };
                    }
                }
            }

            return weights;
        }

        public static void PrintBellmanFordResult(BellmanFordResult[] results, string[] nodeName)
        {
            foreach (var result in results)
            {
                if (result is NegativeCycleResult)
                {
                    Console.WriteLine($"Negative cycle exists with weight { string.Format("{0:N4}", result.Weight) } for: {string.Join("=>", result.Paths.Select(x => $"[{nodeName[x]}]"))}");
                }
                else
                {
                    Console.WriteLine($"Minimum weight is { string.Format("{0:N4}", result.Weight) } for path: {string.Join("=>", result.Paths.Select(x => $"[{nodeName[x]}]"))}");
                }
            }
        }

        public static void PrintMatrix(double[][] grid)
        {
            var rows = grid.Length;
            for (var i = 0; i < rows; i++)
            {
                Console.WriteLine(string.Join("\t", grid[i].Select(x => x == double.MaxValue ? "INF" : string.Format("{00:N3}", x))));
            }
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Bellman Ford Sample ***");
            RunSample1();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /* https://www2.cs.arizona.edu/classes/cs545/fall09/ShortestPath2.prn.pdf             
             		    A 		B 		C 		D 		E
                A		0 		-1		4		INF		INF
                B		INF		0		3		2		2
                C		INF		INF		0		INF		INF
                D		INF		1		5		0		INF
                E		INF		INF		INF		-3		0
             */

            var nodeNames = new[] { "A", "B", "C", "D", "E" };
            var grid = new double[][]
            {
                new double[] { 0.0, -1.0, 4.0, double.MaxValue, double.MaxValue },
                  new double[] { double.MaxValue, 0.0, 3.0, 2.0, 2.0 },
                    new double[] { double.MaxValue, double.MaxValue, 0.0, double.MaxValue, double.MaxValue },
                      new double[] { double.MaxValue, 1.0, 5.0, 0, double.MaxValue },
                        new double[] { double.MaxValue, double.MaxValue, double.MaxValue, -3.0, 0 },
            };            

            Console.WriteLine("Original Adjacency Matrix for given nodes.");
            Console.WriteLine(string.Join("\t", nodeNames));
            PrintMatrix(grid);

            var result = Solve(grid);
            Console.WriteLine("");
            PrintBellmanFordResult(result, nodeNames);
        }      
    }
}
