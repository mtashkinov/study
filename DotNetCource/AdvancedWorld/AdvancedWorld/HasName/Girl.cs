using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld.HasName
{
    [Couple(Pair = "Student", Probability = 0.7, ChildType = "Girl")]
    [Couple(Pair = "Botan", Probability = 0.3, ChildType = "SmartGirl")]
    internal class Girl : Human, IHasName
    {
        public Girl(string name) : base(name, Sex.Woman)
        {
            this.Patronymic = null;
        }

        internal Girl(string name, string patronymic) : base(name, Sex.Woman)
        {
            this.Patronymic = patronymic;
        }

        public string Patronymic { get; set; }
        public override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Girl {0} {1}", Name, Patronymic);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
