using System;

namespace AdvancedWorld.HasName
{
    [Couple(Pair = "Girl", Probability = 0.7, ChildType = "SmartGirl")]
    [Couple(Pair = "PrettyGirl", Probability = 1, ChildType = "PrettyGirl")]
    [Couple(Pair = "SmartGirl", Probability = 0.8, ChildType = "Book")]
    internal sealed class Botan : Student, IHasName
    {
        internal Botan(string name) : base(name)
        {
        }
        internal Botan(string name, string patronymic) : base(name, patronymic)
        {
        }

        public override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Botan {0} {1}", Name, Patronymic);
            Console.ForegroundColor = foregroundColor;
        }
    }
}