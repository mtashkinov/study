using System;

namespace Palindrome
{
    internal sealed class PalindromeChecker
    {
        private const string ignoredChars = " _,.:;!?-()\"\'";
        public static bool IsPalindrome(string str)
        {
            if (str == null) return false;

            var start = 0;
            var end = str.Length - 1;
            while (start <= end)
            {
                while (IsDelimiter(str[start]) && (start <= end))
                {
                    ++start;
                }
                while (IsDelimiter(str[end]) && (start <= end))
                {
                    --end;
                }
                if (char.ToLower(str[start]) != char.ToLower(str[end]) && (start <= end))
                {
                    return false;
                }
                ++start;
                --end;
            }
            return true;
        }

        private static bool IsDelimiter(char chr)
        {
            return ignoredChars.Contains(chr.ToString());
        }
    }
}
