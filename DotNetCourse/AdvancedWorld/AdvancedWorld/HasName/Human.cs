using System;

namespace AdvancedWorld.HasName
{
    internal class Human : IHasName
    {
        internal Human(string name, Sex sex)
        {
            if ((name == null) || (name.Equals("")))
            {
                throw new ArgumentException("Invalid human name");
            }
            this.Name = name;
            this.Sex = sex;
        }
        
        public string Name { get; }
        internal Sex Sex { get; }

        public virtual void ToConsole()
        {
            Console.WriteLine("Human {0} {1}", Name, Sex);
        }
    }

    internal enum Sex
    {
        Man,
        Woman
    }
}
