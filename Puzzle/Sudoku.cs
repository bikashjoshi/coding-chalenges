using System;
using System.Collections.Generic;

namespace CodingChallenges.Puzzle
{
    internal class Sudoku
    {
        /* 
         * https://leetcode.com/problems/sudoku-solver/
         */

        private const char BLANK = '.';
        private const int BLOCK_SIZE = 3;

        private static bool HasDuplicates(char[][] array) {
            var rows = array.Length;
            var cols = array[0].Length;

            var hashSet = new HashSet<int>();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (hashSet.Contains(array[i][j]))
                    {
                        return true;
                    }
                    if (array[i][j] != BLANK)
                    {
                        hashSet.Add(array[i][j]);
                    }
                }
            }

            return false;
        }

        private static bool HasDuplicates(char[] array) {
            var hashSet = new HashSet<int>();
            for (var i = 0; i < array.Length; i++)
            {
                if (hashSet.Contains(array[i]))
                {
                    return true;
                }
                if (array[i] != BLANK)
                {
                    hashSet.Add(array[i]);
                }
            }

            return false;
        }

        private static bool HasValidRows(char[][] board) {
            foreach(var row in board)
            {
                if(HasDuplicates(row))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool HasValidCols(char[][] board) {
            var rows = board.Length;            
            for(var col = 0; col < board[0].Length; col++) {
                var cols = new List<char>();
                for (var row = 0; row < board.Length; row++) {
                    cols.Add(board[row][col]);
                }
                
                if(HasDuplicates(cols.ToArray())) {
                    return false;
                }
            }

            return true;
        }

        private static bool HasValidBlock(char[][] board, int blockSize) {
            var rows = board.Length;
            var cols = board[0].Length;

            for(var i = 0; i < rows; i += blockSize) {
                for(var j = 0; j < cols; j += blockSize) {
                    var array = new char[blockSize][];
                    for(var row = 0; row < blockSize; row++) {
                        array[row] = new char[blockSize];
                        for(var col = 0; col < blockSize; col++) {
                            array[row][col] = board[i + row][j + col];
                        }
                    }

                    if(HasDuplicates(array)) {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool HasValidBoard(char[][] board) {
            return HasValidRows(board) && HasValidCols(board) && HasValidBlock(board, BLOCK_SIZE);
        }

        private static Tuple<int, int> FirstBlankRowCol(char[][] board) {
            var rows = board.Length;
            var cols = board[0].Length;
            for(var i = 0; i < rows; i++) {
                for(var j = 0; j < cols; j++) {
                    if (board[i][j] == BLANK) {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return null;
        }

        private static void Solve(char[][] board) {
            var firstBlank = FirstBlankRowCol(board);
            if (firstBlank == null) {
                return;
            }

            var row = firstBlank.Item1;
            var col = firstBlank.Item2;
            for(var i = '1'; i <= '9'; i++) {
                board[row][col] = i;
                var hasValidBoard = HasValidBoard(board);
                // ConsoleHelper.WriteYellow($"Checking board[{row}][{col}] = {i}, board valid: {hasValidBoard}");
                if (hasValidBoard) {
                    Solve(board);
                    var solutionComplete = FirstBlankRowCol(board) == null;
                    if (solutionComplete) {
                        return;
                    }
                }

                board[row][col] = BLANK;
            }
        }

        public static void RunSample() {
            ConsoleHelper.WriteGreen("*** Sudoku Solver for 9 x 9 board ***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        public static void RunSample1()
        {
            var board = new char[][] {
                new char[] { '5', '3', BLANK, BLANK, '7', BLANK, BLANK, BLANK, BLANK },
                new char[] { '6', BLANK, BLANK, '1', '9', '5', BLANK, BLANK, BLANK },
                new char[] { BLANK, '9', '8', BLANK, BLANK, BLANK , BLANK, '6', BLANK },
                new char[] { '8', BLANK, BLANK, BLANK, '6', BLANK, BLANK, BLANK, '3' },
                new char[] { '4', BLANK, BLANK, '8', BLANK, '3', BLANK, BLANK, '1' },
                new char[] { '7', BLANK, BLANK, BLANK, '2', BLANK, BLANK, BLANK, '6' },
                new char[] { BLANK, '6', BLANK, BLANK, BLANK, BLANK, '2', '8', BLANK },
                new char[] { BLANK, BLANK, BLANK, '4', '1', '9', BLANK, BLANK, '5' },
                new char[] { BLANK, BLANK, BLANK, BLANK, '8', BLANK, BLANK, '7', '9' }
            };

            ConsoleHelper.WriteBlue("Given board");
            foreach (var row in board)
            {
                ConsoleHelper.WriteBlue(string.Join(", ", row));
            }
           
            Solve(board);
            ConsoleHelper.WriteGreen("Solved solution");
            foreach (var row in board)
            {
                ConsoleHelper.WriteGreen(string.Join(", ", row));
            }
        }

        public static void RunSample2()
        {
            var board = new char[][] {
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
                new char[] { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK }
            };

            ConsoleHelper.WriteBlue("Given board. All blank.");
            foreach (var row in board)
            {
                ConsoleHelper.WriteBlue(string.Join(", ", row));
            }

            Solve(board);
            ConsoleHelper.WriteGreen("Solved solution");
            foreach (var row in board)
            {
                ConsoleHelper.WriteGreen(string.Join(", ", row));
            }
        }
    }
}
