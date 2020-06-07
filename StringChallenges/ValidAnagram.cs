using System;
using System.Collections.Generic;

namespace CodingChallenges.StringChallenges
{
    internal class ValidAnagram
    {
        /* https://leetcode.com/problems/valid-anagram/ */

        public static bool IsAnagram(string s, string t)
        {
            var characterFrequency = new Dictionary<char, int>();

            if (s.Length != t.Length)
            {
                return false;
            }

            for (var i = 0; i < s.Length; i++)
            {
                var firstOne = s[i];
                var secondOne = t[i];

                if (!characterFrequency.ContainsKey(firstOne))
                {
                    characterFrequency.Add(firstOne, 1);
                }
                else
                {
                    characterFrequency[firstOne]++;
                }

                if (!characterFrequency.ContainsKey(secondOne))
                {
                    characterFrequency.Add(secondOne, -1);
                }
                else
                {
                    characterFrequency[secondOne]--;
                }
            }


            foreach (var v in characterFrequency.Values)
            {
                if (v != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static void RunSample()
        {            
            ConsoleHelper.WriteGreen($"*** Anagram check with character fequency ***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /*
             *  Input: s = "anagram", t = "nagaram"
                Output: true
             */

            var s = "anagram";
            var t = "nagaram";
            Console.WriteLine($"'{s}' and '{t}' are anagrams: {IsAnagram(s, t)}");
        }

        private static void RunSample2()
        {
            /*
             * Input: s = "rat", t = "car"
                Output: false
             */

            var s = "rat";
            var t = "car";
            Console.WriteLine($"'{s}' and '{t}' are anagrams: {IsAnagram(s, t)}");
        }
    }
}
