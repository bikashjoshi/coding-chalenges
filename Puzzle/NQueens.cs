using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.Puzzle
{
    internal class NQueens
    {
        /* N x N board
         * Find the number of ways N Queens can be placed
         * on the board without threatening each other.
         */

        public static int FindWaysToPlaceQueens(int N)
        {
            return FindWaysToPlaceQueensInternal(N, new List<int>());
        }

        private static int FindWaysToPlaceQueensInternal(int N, IList<int> board, string spaces = "")
        {
            const string space_separator = " ";

            if (N == board.Count)
            {
                /* Solution found. */
                ConsoleHelper.WriteBlue(spaces + string.Join(", ", board.ToArray()));
                ConsoleHelper.WriteBlue(spaces + "---------- SOLUTION FOUND ... -----------------");                
                return 1;
            }

            var count = 0;
            for (var i = 0; i < N; i++)
            {
                board.Add(i);
                if (board.Count == 1)
                {
                    // means we are starting with new square on the bottom row
                    ConsoleHelper.WritePink($"Now placing first Queen at {i} column of first row. Count So Far {count}.");                    
                }

                if (IsValid(board, spaces))
                {
                    count += FindWaysToPlaceQueensInternal(N, board, spaces + space_separator);
                }

                ConsoleHelper.WriteYellow(spaces + $"Removing at index {board.Count - 1}");
                board.RemoveAt(board.Count - 1);
            }

            return count;
        }

        private static bool IsValid(IList<int> board, string spaces)
        {
            var current_queen_row = board.Count - 1;
            var current_queen_col = board[board.Count - 1];
            for (var row = 0; row < board.Count - 1; row++)
            {
                var col = board[row];

                var diff = Math.Abs(current_queen_col - col);
                if (diff == 0 || diff == current_queen_row - row) // check column or diagonal (at diagonal differences for row and column are equal)
                {
                    ConsoleHelper.WriteRed($"{spaces}[{current_queen_row}][{current_queen_col}] => FALSE => BOARD: {string.Join(",", board.ToArray())}");
                    return false;
                }
            }

            ConsoleHelper.WriteGreen($"{spaces}[{current_queen_row}][{current_queen_col}] => TRUE => BOARD: {string.Join(",", board.ToArray())}");
            return true;
        }

        public static void RunSample()
        {
            const int N = 4;
            ConsoleHelper.WriteGreen($"*** Queens Puzzle Solution for N={N} ***");
            ConsoleHelper.WritePink($"** Finding Number of Ways Queens Puzzle can be solved for {N} x {N} board. **");
            var numberOfWays = FindWaysToPlaceQueens(N);
            ConsoleHelper.WritePink($"There are total {numberOfWays} ways to place Queens in {N} x {N} board.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
