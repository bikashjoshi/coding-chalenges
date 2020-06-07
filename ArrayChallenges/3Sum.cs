using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.ArrayChallenges
{
    internal class _3Sum
    {
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var list = new List<IList<int>>();
            var hashSet = new HashSet<string>();
            var dictionary = new Dictionary<int, int>();

            if (nums.Length < 3)
            {
                return list;
            }

            foreach (var num in nums)
            {
                if (dictionary.ContainsKey(num))
                {
                    dictionary[num]++;
                }
                else
                {
                    dictionary.Add(num, 1);
                }
            }

            if (dictionary.Count == 1)
            {
                var firstItem = dictionary.FirstOrDefault();

                if (firstItem.Key == 0 && firstItem.Value >= 3)
                {
                    list.Add(new List<int>(new[] { 0, 0, 0 }));
                    return list;
                }
            }

            for (int i = 0; i < nums.Length - 2; i++)
            {
                var a = nums[i];
                dictionary[a]--;
                for (int j = i + 1; j < nums.Length - 1; j++)
                {
                    var b = nums[j];
                    dictionary[b]--;
                    var sum2 = a + b;
                    var c = -sum2;

                    if (dictionary.ContainsKey(c) && !hashSet.Contains(getKey(a, b, c)) && dictionary[c] >= 1)
                    {
                        AddKeys(a, b, c, hashSet);
                        list.Add(new List<int> { a, b, c });
                    }

                    dictionary[b]++;
                }

                dictionary[a]++;
            }

            return list;
        }

        private static void AddKeys(int a, int b, int c, HashSet<string> set)
        {
            set.Add(getKey(a, b, c));
            set.Add(getKey(a, c, b));
            set.Add(getKey(b, a, c));
            set.Add(getKey(b, c, a));
            set.Add(getKey(c, b, a));
            set.Add(getKey(c, a, b));
        }

        private static string getKey(int a, int b, int c)
        {
            return $"{a}#{b}#{c}";
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Three Sum Sample ***");
            var input = new[] { 2, 0, -2, -5, -5, -3, 2, -4 };
            var result = ThreeSum(input);
            Console.WriteLine($"Input array is {string.Join(", ", input)}.");
            Console.WriteLine("The resulting group of 3 elements with sum = zero are:");
            foreach(var r in result)
            {
                Console.WriteLine(string.Join(", ", r));
            }
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
