using System.Collections.Generic;

namespace CodingChallenges.Puzzle
{
    internal class TowersOfHanoi
    {
        /* https://www.hackerrank.com/contests/launchpad-1-winter-challenge/challenges/shift-plates/problem */
        public static void SolveTowers(int count, string source, string spare, string destination, Dictionary<string, List<int>> poles, string textPrefix = "")
        {            
            if (count >= 1)
            {
                SolveTowers(count - 1, source, destination, spare, poles, textPrefix + "  ");

                ConsoleHelper.WriteYellow($"{textPrefix}Moving ring {count} from {source} to {destination}");
                poles[destination].Insert(0, count);
                poles[source].Remove(count);
                ConsoleHelper.WriteRed($"{textPrefix}{source}:{string.Join(", ", poles[source])}");
                ConsoleHelper.WriteGreen($"{textPrefix}{destination}:{string.Join(", ", poles[destination])}");
                ConsoleHelper.WriteBlue($"{textPrefix}{spare}:{string.Join(", ", poles[spare])}");

                SolveTowers(count - 1, spare, source, destination, poles, textPrefix + "  ");
            }
        }

        public static void RunSample()
        {
            const string SOURCE = "SRC";
            const string SPARE = "SPARE";
            const string DESTINATION = "DEST";
            ConsoleHelper.WriteGreen("*** Towers of Hanoi Sample ***");
            SolveTowers(4, SOURCE, SPARE, DESTINATION, 
                new Dictionary<string, List<int>>
                {
                    { SOURCE, new List<int> { 1, 2, 3, 4 } },
                    { SPARE, new List<int>() },
                    { DESTINATION, new List<int>() }
                });
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
