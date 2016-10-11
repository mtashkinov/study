using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld.HasName
{
    internal sealed class Book : IHasName
    {
        public Book(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        void IHasName.ToConsole()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("PrettyGirl {0}", Name);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
