using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{
    internal class Parent : Human
    {
        internal Parent(int age, string name, Sex sex, int childsNum) : base(age, name, sex)
        {
            if (childsNum < 0)
            {
                throw new ArgumentException("Invalid childs number");
            }
            this.ChildsNum = childsNum;
        }

        internal int ChildsNum { get; }

        internal override void ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Parent {0}, {1}, age {2}, {3} childs", Name, Sex, Age, ChildsNum);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
