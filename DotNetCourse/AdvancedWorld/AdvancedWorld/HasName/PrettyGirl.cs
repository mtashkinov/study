using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld.HasName
{
    [Couple(Pair = "Student", Probability = 0.4, ChildType = "Girl")]
    [Couple(Pair = "Botan", Probability = 0.1, ChildType = "Book")]
    internal sealed class PrettyGirl : Girl, IHasName
    {
        public PrettyGirl(string name) : base(name)
        {
        }

        internal PrettyGirl(string name, string patronymic) : base(name, patronymic)
        {
        }
        public override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("PrettyGirl {0} {1}", Name, Patronymic);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
