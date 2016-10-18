using System;
namespace World
{
    internal class Student : Human
    {
        internal Student(int age, string name, Sex sex, string patronymic) : base(age, name, sex)
        {
            if (String.IsNullOrEmpty(patronymic))
            {
                throw new ArgumentException("Invalid patronymic");
            }
            this.Patronymic = patronymic;
        }

        internal string Patronymic { get; }
        internal override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Student {0} {1}, {2}, age {3}", Name, Patronymic, Sex, Age);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
