using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.StringChallenges
{
    internal class GroupingAnagram
    {    
        /* https://leetcode.com/problems/group-anagrams/ */

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {            
            var dictionary = new Dictionary<string, HashSet<string>>();            
            dictionary.Add(SortString(strs[0]), new HashSet<string> { strs[0] });
            var indexesToSkip = new HashSet<int>();

            for (int i = 1; i < strs.Length; i++)
            {
                var secondItem = strs[i];
                var newKey = SortString(secondItem);

                if (dictionary.ContainsKey(newKey))
                {
                    dictionary[newKey].Add(secondItem);
                }
                else
                {
                    dictionary.Add(newKey, new HashSet<string> { secondItem });
                }
            }
            
            var list = new List<IList<string>>();
            foreach(var v in dictionary.Values)
            {
                list.Add(v.ToList());
            }

            return list;
        }

        private static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }       

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Grouping Anagram ***");
            var strs = new[] { "eat", "tea", "tan", "ate", "nat", "bat" };
            var result = GroupAnagrams(strs);

            Console.WriteLine($"Input Array: { string.Join(", ", strs) }");
            foreach (var r in result)
            {
                Console.WriteLine($"Group: {string.Join(", ", r)}");
            }

            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
