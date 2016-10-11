using System;

namespace World
{
    internal sealed class Botan : Student
    {
        internal Botan(int age, string name, Sex sex, string patronymic, double avgMark) : base(age, name, sex, patronymic)
        {
            if ((avgMark < 0) && (avgMark > 5))
            {
                throw new ArgumentException("Invalid average mark");
            }
            this.AverageMark = avgMark;
        }

        internal double AverageMark { get; }

        internal override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Botan {0} {1}, {2}, age {3}, {4} average mark", Name, Patronymic, Sex, Age, AverageMark);
            Console.ForegroundColor = foregroundColor;
        }
    }
}