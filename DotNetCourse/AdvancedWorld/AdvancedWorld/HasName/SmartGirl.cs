using System;

namespace AdvancedWorld.HasName
{
    [Couple(Pair = "Student", Probability = 0.2, ChildType = "Girl")]
    [Couple(Pair = "Botan", Probability = 0.5, ChildType = "Book")]
    internal sealed class SmartGirl : Girl, IHasName
    {
        public SmartGirl(string name) : base(name)
        {
        }

        internal SmartGirl(string name, string patronymic) : base(name, patronymic)
        {
        }
        public override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SmartGirl {0} {1}", Name, Patronymic);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
