using System;
namespace AdvancedWorld.HasName
{
    [Couple(Pair = "Girl", Probability = 0.7, ChildType = "Girl")]
    [Couple(Pair = "PrettyGirl", Probability = 1, ChildType = "PrettyGirl")]
    [Couple(Pair = "SmartGirl", Probability = 0.5, ChildType = "Girl")]
    internal class Student : Human, IHasName
    {
        internal Student(string name) : base(name, Sex.Man)
        {
            this.Patronymic = null;
        }

        internal Student(string name, string patronymic) : base(name, Sex.Man)
        {
            this.Patronymic = patronymic;
        }
        public string Patronymic { get; set; }
        public override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Student {0} {1}", Name, Patronymic);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
