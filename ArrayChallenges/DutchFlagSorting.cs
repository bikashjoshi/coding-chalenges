using System;

namespace CodingChallenges.ArrayChallenges
{
    internal class DutchFlagSorting
    {
        public static void Sort(char[] array)
        {
            var redIndex = 0;
            var greenIndex = 0;
            var blueIndex = array.Length - 1;
            while (greenIndex <= blueIndex)
            {
                var c = array[greenIndex];
                if (c == 'R')
                {
                    Swap(array, redIndex, greenIndex);
                    redIndex++;
                    greenIndex++;
                }
                else if (c == 'G')
                {
                    greenIndex++; // Green immediately next to Red
                }
                else if (c == 'B') // Add Blue on the right edge
                {
                    Swap(array, blueIndex, greenIndex);
                    blueIndex--;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        private static void Swap(char[] array, int i, int j)
        {
            if (i == j)
                return;

            char temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** DUTCH FLAG SORTING ***");
            var array = new char[] { 'G', 'B', 'R', 'R', 'G', 'B', 'B', 'R', 'G' };
            ConsoleHelper.WriteBlue($"Input array is: {string.Join(", ", array)}");            
            Sort(array);
            ConsoleHelper.WritePink($"Sorted result: {string.Join(", ", array)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
