using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class WordSearch
    {
        /*  
         *  https://leetcode.com/problems/word-search/
         */

        private class Coordinate
        {
            public int X { get; set; }

            public int Y { get; set; }          
        }

        private struct StackItem
        {
            public Coordinate Position { get; set; }

            public List<Coordinate> Visited { get; set; }

            public string TextSoFar { get; set; }
        }

        private static bool HasVisited(List<Coordinate> list, Coordinate position)
        {
            return list.FirstOrDefault(x => x.X == position.X && x.Y == position.Y) != null;
        }

        private static IEnumerable<Coordinate> GetNeighbors(Coordinate position, int rowBound, int columnBound)
        {
            var neighbors = new List<Coordinate>();
            if (position.Y - 1 >= 0)
            {
                var left = new Coordinate
                {
                    X = position.X,
                    Y = position.Y - 1
                };

                neighbors.Add(left);
            }

            if (position.Y + 1 < columnBound)
            {
                var right = new Coordinate
                {
                    X = position.X,
                    Y = position.Y + 1
                };

                neighbors.Add(right);
            }

            if (position.X - 1 >= 0)
            {
                var top = new Coordinate
                {
                    X = position.X - 1,
                    Y = position.Y
                };
                neighbors.Add(top);
            }

            if (position.X + 1 < rowBound)
            {
                var bottom = new Coordinate
                {
                    X = position.X + 1,
                    Y = position.Y
                };

                neighbors.Add(bottom);
            }

            return neighbors;
        }

        private static void TryEnqueueElement(Stack<StackItem> stack, StackItem previousItem, Coordinate currentElement, char currentChar, string wordToSearch)
        {
            var nextWord = previousItem.TextSoFar + currentChar;
            if (wordToSearch.Contains(nextWord))
            {
                stack.Push(new StackItem
                {
                    Position = currentElement,
                    Visited = new List<Coordinate>(previousItem.Visited) { currentElement },
                    TextSoFar = nextWord
                });
            }
        }

        public static bool Exist(char[][] board, string word)
        {
            var rowBound = board.GetLength(0);
            var columnBound = board[0].Length;
            var globalVisitedList = new List<Coordinate>();

            for (int x = rowBound - 1; x >= 0; x--)
            {
                for (int y = columnBound - 1; y >= 0; y--)
                {
                    var start = new Coordinate { X = x, Y = y };

                    var firstChar = board[x][y].ToString();
                    if (!word.StartsWith(firstChar))
                    {
                        if (!word.Contains(firstChar))
                        {
                            globalVisitedList.Add(start);
                        }

                        continue;
                    }

                    var stack = new Stack<StackItem>();

                    stack.Push(new StackItem
                    {
                        Position = start,
                        Visited = new List<Coordinate> { start },
                        TextSoFar = firstChar
                    });

                    while (stack.Count != 0)
                    {
                        var item = stack.Pop();

                        if (item.TextSoFar == word)
                        {
                            ConsoleHelper.WriteYellow($"Path Found: {string.Join(" => ", item.Visited.Select(c => $"[{c.X}][{c.Y}({board[c.X][c.Y]})]"))}");
                            return true;
                        }

                        var neighbors = GetNeighbors(item.Position, rowBound, columnBound);
                        foreach (var neighbor in neighbors)
                        {
                            if (!HasVisited(globalVisitedList, neighbor) && !HasVisited(item.Visited, neighbor)) // visited list is connected path
                            {
                                var next = board[neighbor.X][neighbor.Y];
                                if (!word.Contains(next))
                                {
                                    globalVisitedList.Add(neighbor);
                                }
                                else
                                {
                                    TryEnqueueElement(stack, item, neighbor, next, word);
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Word search in a board ***");
            var board = new char[][]
                { new[] { 'A', 'B', 'C', 'E' },
                  new[] { 'S', 'F', 'C', 'S' },
                  new[] { 'A', 'D', 'E', 'E' } };

            var word1 = "ABCCED";
            var word2 = "SEE";
            var word3 = "ABCB";
            foreach(var row in board)
            {
                Console.WriteLine(string.Join(", ", row));
            }

            Console.WriteLine($"{word1} exist on the board: {Exist(board, word1)}");
            Console.WriteLine($"{word2} exist on the board: {Exist(board, word2)}");
            Console.WriteLine($"{word3} exist on the board: {Exist(board, word3)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
