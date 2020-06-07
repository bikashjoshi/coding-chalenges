using System;

namespace CodingChallenges.DynamicProgramming
{
    internal class MaximalSquare
    {
        /* https://leetcode.com/problems/maximal-square/
         * Given a 2D binary matrix filled with 0's and 1's, 
         * find the largest square containing only 1's and return its area.
         */

        public static int GetMaximalSquare(char[][] matrix)
        {
            var rows = matrix.Length;
            if (rows == 0)
            {
                return 0;
            }

            var cols = matrix[0].Length;

            var areaMatrix = new int[rows + 1][];
            for (var i = 0; i < rows + 1; i++)
            {
                areaMatrix[i] = new int[cols + 1];
            }

            var size = 0;
            for (var i = 1; i <= rows; i++)
            {
                for (var j = 1; j <= cols; j++)
                {
                    if (matrix[i - 1][j - 1] == '0')
                        continue;
                    var min = Math.Min(areaMatrix[i - 1][j - 1], Math.Min(areaMatrix[i][j - 1], areaMatrix[i - 1][j]));
                    areaMatrix[i][j] = min + 1;
                    var newSize = areaMatrix[i][j];
                    size = Math.Max(size, newSize);
                }
            }

            for (var i = 0; i < rows + 1; i++)
            {
                ConsoleHelper.WriteYellow(string.Join(", ", areaMatrix[i]));
            }

            return size * size;
        }

        public static void RunSample()
        {
            /*
             * Input: 
                1 0 1 0 0
                1 0 1 1 1
                1 1 1 1 1
                1 0 0 1 0
               Output: 4
             */
            var input = new char[][] {
                new char[] { '1', '0', '1', '0', '0' },
                new char[] { '1', '0', '1', '1', '1' },
                new char[] { '1', '1', '1', '1', '1' },
                new char[] { '1', '0', '0', '1', '0' }
            };

            ConsoleHelper.WriteGreen("*** Maximal Square ***");
            Console.WriteLine("Input Array:");
            foreach (var row in input)
            {
                Console.WriteLine(string.Join(",", row));
            }
            var output = GetMaximalSquare(input);            
            
            Console.WriteLine($"Maximal Square Area is: {string.Join(",", output)}.");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
