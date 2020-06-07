using System;
using System.Collections.Generic;

namespace CodingChallenges.GraphChallenges
{
    internal class Island
    {
        /* https://leetcode.com/problems/number-of-islands/ */

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                var coordinate = obj as Coordinate;
                return coordinate != null &&
                       X == coordinate.X &&
                       Y == coordinate.Y;
            }

            public override int GetHashCode()
            {
                var hashCode = 1861411795;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                return hashCode;
            }
        }

        private static bool HasNotVisited(HashSet<Coordinate> visited, Coordinate x)
        {
            return !visited.Contains(x);
        }

        private static void PushNeighbors(Stack<Coordinate> stack, char[][] grid, int x, int y, int rowBound, int columnBound, HashSet<Coordinate> visited)
        {
            var nextX = x - 1;
            var nextY = y;
            var c1 = new Coordinate { X = nextX, Y = nextY };
            if (nextX >= 0 && grid[nextX][nextY] == '1' && HasNotVisited(visited, c1))
            {                
                stack.Push(c1);
                visited.Add(c1);
            }

            nextX = x + 1;
            nextY = y;
            var c2 = new Coordinate { X = nextX, Y = nextY };
            if (nextX < rowBound && grid[nextX][nextY] == '1' && HasNotVisited(visited, c2))
            {                
                stack.Push(c2);
                visited.Add(c2);
            }

            nextX = x;
            nextY = y - 1;
            var c3 = new Coordinate { X = nextX, Y = nextY };
            if (y - 1 >= 0 && grid[nextX][nextY] == '1' && HasNotVisited(visited, c3))
            {                
                stack.Push(c3);
                visited.Add(c3);
            }

            nextX = x;
            nextY = y + 1;
            var c4 = new Coordinate { X = nextX, Y = nextY };
            if (nextY < columnBound && grid[nextX][nextY] == '1' && HasNotVisited(visited, c4))
            {                
                stack.Push(c4);
                visited.Add(c4);
            }
        }

        public static int NumIslands(char[][] grid)
        {
            var rows = grid.Length;
            if (rows == 0)
            {
                return 0;
            }

            var columns = grid[0].Length;
            var visited = new HashSet<Coordinate>();
           
            int counter = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (grid[i][j] == '0')
                    {
                        continue;
                    }

                    var coordinate = new Coordinate { X = i, Y = j };
                    if (HasNotVisited(visited, coordinate))
                    {
                        var stack = new Stack<Coordinate>();                        
                        visited.Add(coordinate);
                        stack.Push(coordinate);
                        while (stack.Count != 0)
                        {
                            var next = stack.Pop();
                            PushNeighbors(stack, grid, next.X, next.Y, rows, columns, visited);
                        }

                        counter++;
                    }
                }
            }

            return counter;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Running Island Sample ***");
            char[][] grid = new char [][] {
                new char [] { '1', '1', '1', '1', '0' },
                new char [] { '1', '1', '0', '1', '0' },
                new char [] { '1', '1', '0', '0', '0' },
                new char [] { '0', '0', '0', '0', '0' }
            };

            foreach(var row in grid)
            {
                Console.WriteLine(string.Join(", ", row));
            }

            var islands = NumIslands(grid);
            Console.WriteLine($"Total number of island = { islands}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
