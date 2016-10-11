using System;

namespace RegexpValidator
{
    internal sealed class Program
    {
        private const string descriptionOutput = "This program validates e-mails, Rus and US zip codes and Russian phones. Pass empty string to exit";
        private const string askStringOutput = "Please enter string to check";

        static void Main()
        {
            var timeToExit = false;
            Console.WriteLine(descriptionOutput);
            do
            {
                Console.WriteLine(askStringOutput);
                var input = Console.ReadLine();
                if (!input.Equals(""))
                {
                    Console.WriteLine(Validator.Validate(input));
                }
                else
                {
                    timeToExit = true;
                }
            } while (!timeToExit);
        }
    }
}