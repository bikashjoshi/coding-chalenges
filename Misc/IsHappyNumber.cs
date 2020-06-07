using System;
using System.Collections.Generic;

namespace CodingChallenges.Misc
{
    internal class IsHappyNumber
    {
        static bool IsHappy(int n)
        {
            var hashSet = new HashSet<int>();
            while (true)
            {
                if (n == 1)
                {
                    return true;
                }

                if (n == 0)
                {
                    return false;
                }

                var sum = 0;
                var k = n;
                var totalDigits = CountDigit(n);
                for (var i = totalDigits; i > 0; i--)
                {
                    var digit = (int)(k * Math.Pow(10, -i + 1));
                    k -= digit * (int)Math.Pow(10, i - 1);
                    sum += digit * digit;
                }

                if (hashSet.Contains(sum))
                {
                    return false;
                }

                hashSet.Add(sum);
                n = sum;
            }
        }

        private static int CountDigit(long n)
        {
            int count = 0;
            while (n != 0)
            {
                n = n / 10;
                ++count;
            }
            return count;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Happy Number Sample ***");
            var n = 19;
            var h = IsHappy(n);
            Console.WriteLine($"{n} is happy number: {h}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
