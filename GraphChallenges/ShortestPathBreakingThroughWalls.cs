using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class ShortestPathBreakingThroughWalls
    {
        /*
         * https://leetcode.com/discuss/interview-question/353827/Google-or-Onsite-or-Shortest-Path-Breaking-Through-Walls
         * Given a 2D grid of size r * c. 0 is walkable, and 1 is a wall. 
         * You can move up, down, left or right at a time. Now you are allowed 
         * to break at most 1 wall, what is the minimum steps to walk from the upper 
         * left corner (0, 0) to the lower right corner (r-1, c-1)?
         */

        private struct Coordinate
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public static bool operator ==(Coordinate element1, Coordinate element2)
            {
                return element1.Row == element2.Row
                       && element1.Col == element2.Col;
            }

            public static bool operator !=(Coordinate element1, Coordinate element2)
            {
                var areEqual = element1.Row == element2.Row
                       && element1.Col == element2.Col;
                return !areEqual;
            }
        }

        private struct QueueItem
        {
            public Coordinate Element { get; set; }

            public int NumberOfWallsBroken { get; set; }

            public List<Coordinate> ConnectedPath { get; set; }
        }

        private static bool ContainsElement(List<Coordinate> list, Coordinate element)
        {
            return list.FirstOrDefault(x => x == element) != default(Coordinate);
        }

        private static IEnumerable<Coordinate> GetNeighbors(Coordinate element, int rowBound, int columnBound)
        {
            var neighbors = new List<Coordinate>();
            var left = new Coordinate
            {
                Row = element.Row,
                Col = element.Col - 1
            };

            var right = new Coordinate
            {
                Row = element.Row,
                Col = element.Col + 1
            };

            var top = new Coordinate
            {
                Row = element.Row - 1,
                Col = element.Col
            };

            var bottom = new Coordinate
            {
                Row = element.Row + 1,
                Col = element.Col
            };

            if (left.Col >= 0)
            {
                neighbors.Add(left);
            }

            if (right.Col < columnBound)
            {
                neighbors.Add(right);
            }

            if (top.Row >= 0)
            {
                neighbors.Add(top);
            }

            if (bottom.Row < rowBound)
            {
                neighbors.Add(bottom);
            }

            return neighbors;
        }

        private static void TryEnqueueElement(Queue<QueueItem> queue, QueueItem previousItem, Coordinate currentElement, int currentElementValue, int maximumWallsToBreak)
        {
            if (currentElementValue == 0)
            {
                queue.Enqueue(new QueueItem
                {
                    Element = currentElement,
                    NumberOfWallsBroken = previousItem.NumberOfWallsBroken,
                    ConnectedPath = new List<Coordinate>(previousItem.ConnectedPath) { currentElement }
                });
            }
            else if (currentElementValue == 1 && previousItem.NumberOfWallsBroken < maximumWallsToBreak)
            {
                queue.Enqueue(new QueueItem
                {
                    Element = currentElement,
                    NumberOfWallsBroken = previousItem.NumberOfWallsBroken + 1,
                    ConnectedPath = new List<Coordinate>(previousItem.ConnectedPath) { currentElement }
                });
            }
        }

        private static IEnumerable<Coordinate> FindShortestPath(Coordinate start, Coordinate destination, int[,] grid, int maximumWallsToBreak)
        {
            var queue = new Queue<QueueItem>();

            var rowBound = grid.GetLength(0);
            var columnBound = grid.GetLength(1);

            queue.Enqueue(new QueueItem
            {
                Element = start,
                NumberOfWallsBroken = 0,
                ConnectedPath = new List<Coordinate> { start }
            });

            while (queue.Count != 0)
            {
                var item = queue.Dequeue();

                if (item.Element == destination)
                {
                    return item.ConnectedPath;
                }

                var neighbors = GetNeighbors(item.Element, rowBound, columnBound);
                foreach (var neighbor in neighbors)
                {
                    if (!ContainsElement(item.ConnectedPath, neighbor)) // visited list is connected path
                    {
                        TryEnqueueElement(queue, item, neighbor, grid[neighbor.Row, neighbor.Col], maximumWallsToBreak);
                    }
                }
            }

            return Enumerable.Empty<Coordinate>();
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Shortest Path Breaking Through Walls ***");
            RunTest1();
            RunTest2();
            RunTest3();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunTest1()
        {
            ConsoleHelper.WritePink("Running Sample 1 with 1 wall possible wall to break.");

            var grid = new[,] { { 0, 1, 0, 0, 0 },
                                { 0, 0, 0, 1, 0 },
                                { 1, 1, 0, 1, 0 },
                                { 1, 1, 1, 1, 0 } };

            /*
             * Output: 7
                Explanation: Change `1` at (0, 1) to `0`, the shortest path is as follows:
                (0, 0) -> (0, 1) -> (0, 2) -> (0, 3) -> (0, 4) -> (1, 4) -> (2, 4) -> (3, 4)
             */
            RunSampleStep(grid, 1);
        }

        private static void RunTest2()
        {
            ConsoleHelper.WritePink("Running Sample 2 with 1 wall possible wall to break.");

            var grid = new[,] { { 0, 1, 1 },
                                { 1, 1, 0 },
                                { 1, 1, 0 } };

            /*  Output: -1
                Explanation: Regardless of which `1` is changed to `0`, there is no viable path.
             */
            RunSampleStep(grid, 1);
        }

        private static void RunTest3()
        {
            ConsoleHelper.WritePink("Running Sample 3 with 2 possible walls to break.");

            var grid = new[,] { { 0, 1, 0, 0, 0 },
                                { 0, 0, 0, 1, 0 },
                                { 0, 1, 1, 1, 1 },
                                { 0, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 0 } };
            /* k = 2
             * Output: 10
               Explanation: Change (2, 4) and (3, 4) to `0`.
               Route (0, 0) -> (1, 0) -> (1, 1) -> (1, 2) -> (0, 2) -> (0, 3) -> (0, 4) -> (1, 4) -> (2, 4) -> (3, 4) -> (4, 4)
             */

            RunSampleStep(grid, 2);
        }

        private static void RunSampleStep(int[,] grid, int maximumWallsToBreak)
        {
            var rowBound = grid.GetLength(0);
            var columnBound = grid.GetLength(1);

            var start = new Coordinate { Row = 0, Col = 0 };
            var destination = new Coordinate { Row = rowBound - 1, Col = columnBound - 1 };

            PrintMatrix(grid);
            Console.WriteLine("\r\n");

            var paths = FindShortestPath(start, destination, grid, maximumWallsToBreak);
            if (!paths.Any())
            {
                ConsoleHelper.WriteRed("There is no visible path.");
                Console.WriteLine(Environment.NewLine);                
            }
            else
            {
                PrintGridElements(paths);
            }
        }

        private static void PrintMatrix(int[,] matrix, ConsoleColor color = ConsoleColor.Yellow)
        {
            var rowBound = matrix.GetLength(0);
            var columnBound = matrix.GetLength(1);
            Console.ForegroundColor = color;
            Console.WriteLine("Input matrix is: ");
            Console.WriteLine("[");
            for (int row = 0; row < rowBound; row++)
            {
                Console.Write("  [");
                for (int col = 0; col < columnBound; col++)
                {
                    Console.Write($"\t{matrix[row, col]}");
                }
                Console.Write("  ]\r\n");
            }

            Console.Write("]");
            Console.ResetColor();
        }

        private static void PrintGridElements(IEnumerable<Coordinate> gridElements, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            var count = 0;
            foreach (var element in gridElements)
            {
                Console.Write($"[{element.Row}][{element.Col}] => ");
                count++;
            }

            Console.Write("GOAL!");
            Console.WriteLine("\r\n");
            Console.WriteLine($"Total Steps: {count - 1}");
            Console.WriteLine("\r\n");
            Console.ResetColor();
        }
    }
}
