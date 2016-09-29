using System;

namespace Palindrome
{
    internal sealed class Program
    {
        private const string descriptionOutput = "This program checks if input string a palindrome. Pass empty string to stop";
        private const string askStringOutput = "Please input string to check:";
        private const string positiveResultOutput = "Looks like it's palindrome";
        private const string negativeResultOutput = "Looks like it isn't palindrome";

        static void Main()
        {
            var timeToExit = false;
            Console.WriteLine(descriptionOutput);
            do
            {
                Console.WriteLine(askStringOutput);
                var inputStr = Console.ReadLine();
                if (!inputStr.Equals(""))
                {
                    if (PalindromeChecker.IsPalindrome(inputStr))
                    {
                        Console.WriteLine(positiveResultOutput);
                    }
                    else
                    {
                        Console.WriteLine(negativeResultOutput);
                    }
                }
                else
                {
                    timeToExit = true;
                }
            } while (!timeToExit);
        }
    }
}
