using System;
using System.Collections.Generic;

namespace CodingChallenges.StackAndQueueChallenges
{
    internal class CheckValidString
    {
        /* https://leetcode.com/problems/valid-parenthesis-string/
         */

        public static bool IsValid(string s)
        {
            var openParenthesis = new Stack<int>();
            var asterisk = new Stack<int>();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (c == '(')
                {
                    openParenthesis.Push(i);
                }
                else if (c == '*')
                {
                    asterisk.Push(i);
                }
                else if (c == ')')
                {
                   if (openParenthesis.Count > 0)
                    {
                        openParenthesis.Pop();
                    }
                    else if (asterisk.Count > 0)
                    {
                        asterisk.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            while (openParenthesis.Count != 0 && asterisk.Count != 0)
            {
                if (openParenthesis.Pop() > asterisk.Pop())
                {
                    return false;
                }
            }

            return openParenthesis.Count == 0;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Valid Parenthesis check Sample ***");
            string text = "(*))";
            Console.WriteLine($"Parenthesis on {text} is valid: {IsValid(text)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
