using System;

namespace World
{
    internal sealed class CoolParent : Parent
    {
        internal CoolParent(int age, string name, Sex sex, int childsNum, int money) : base(age, name, sex, childsNum)
        {
            if (money < 0)
            {
                throw new ArgumentException("Invalid money");
            }
            this.Money = money;
        }

        internal int Money { get; }

        internal override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("CoolParent {0}, {1}, age {2}, {3} childs ", Name, Sex, Age, ChildsNum);
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("${0}", Money);
            Console.BackgroundColor = background;
            Console.ForegroundColor = foregroundColor;
        }
    }
}
