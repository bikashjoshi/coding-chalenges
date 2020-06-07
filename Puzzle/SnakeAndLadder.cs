using System;
using System.Collections.Generic;

namespace CodingChallenges.Puzzle
{
    internal class SnakeAndLadder
    {
        /* Find the smallest number of turns it takes to play snakes and ladders.
        */       

        public static Tuple<int, string> FindBfs(int start, int end, int[][] snakes, int[][] ladders)
        {
            var board = new Dictionary<int, int>();
            for (int i = 0; i <= 100; i++)
            {
                board[i] = i;
            }

            foreach (var snake in snakes)
            {
                var snakeTop = snake[0];
                var snakeBottom = snake[1];
                board[snakeTop] = snakeBottom;
            }

            foreach (var ladder in ladders)
            {
                var ladderBottom = ladder[0];
                var ladderTop = ladder[1];
                board[ladderBottom] = ladderTop;
            }

            var turns = 0;

            var visited = new List<int>();
            string plays = string.Empty;
            var constructedPath = new Dictionary<int, Dictionary<int, List<int>>>();
            var path = new Queue<Tuple<int, int, string>>();
            path.Enqueue(Tuple.Create(start, turns, "0"));

            var found = false;
            while (path.Count != 0 && !found)
            {
                var nextPath = path.Dequeue();
                var square = nextPath.Item1;
                turns = nextPath.Item2;
                var soFar = nextPath.Item3;

                // six possible moves
                for (var move = square + 1; move <= square + 6; move++)
                {
                    AddInConstructedPath(constructedPath, turns, square, move);

                    if (move >= end)
                    {
                        turns++;
                        found = true;
                        soFar += "->" + move;
                        plays = soFar;
                        break;
                    }

                    if (!visited.Contains(move))
                    {
                        visited.Add(move);
                        path.Enqueue(Tuple.Create(board[move], turns + 1, soFar + "->" + board[move]));
                    }
                }
            }

            PrintAllPathsTaken(constructedPath);

            return Tuple.Create(turns, plays);
        }

        private static void AddInConstructedPath(Dictionary<int, Dictionary<int, List<int>>> constructedPath, int turn, int square, int move)
        {
            if (!constructedPath.ContainsKey(turn))
            {
                constructedPath[turn] = new Dictionary<int, List<int>>();
            }

            if (!constructedPath[turn].ContainsKey(square))
            {
                constructedPath[turn][square] = new List<int>();
            }

            constructedPath[turn][square].Add(move);
        }

        private static void PrintAllPathsTaken(Dictionary<int, Dictionary<int, List<int>>> constructedPath)
        {
            ConsoleHelper.WriteBlue("Here are all of the possible paths on each turn.");
            string spaces_separator = string.Empty;
            foreach (var turnsKvp in constructedPath)
            {
                ConsoleHelper.WritePink($"{spaces_separator}Turn: {turnsKvp.Key}");
                foreach (var moves in turnsKvp.Value)
                {
                    ConsoleHelper.WriteYellow($"{spaces_separator}Possible moves from {moves.Key}: " + string.Join(", ", moves.Value));
                }

                spaces_separator += "  ";
            }          
        }           
       
        public static void RunSample()
        {
            Console.WriteLine("*** Snake and Ladder ***");
            var snakes = new int[][]
            {                
                new int[] { 57, 40 },
                new int[] { 62, 22 },
                new int[] { 71, 1 },
                new int[] { 75, 5 },
                new int[] { 88, 18 },
                new int[] { 95, 51 },
                new int[] { 97, 79 },
            };

            var ladders = new int[][]
            {
                new int[] { 28, 84 },
                new int[] { 58, 77 },
                new int[] { 75, 86 },
                new int[] { 80, 100 },
                new int[] { 90, 91 }
            };

            var start = 0;
            var end = 100;
            var result = FindBfs(start, end, snakes, ladders);
            var turns = result.Item1;
            var plays = result.Item2;
            Console.WriteLine($"In order to solve Snake Ladder, minimum required turns is {turns} and solution is {plays}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
