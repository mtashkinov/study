using System;

namespace World
{
    internal class Human
    {
        internal Human(int age, string name, Sex sex)
        {
            if ((age < 0) || String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid human parameters");
            }
            this.Age = age;
            this.Name = name;
            this.Sex = sex;
        }

        internal int Age { get; }
        internal string Name { get; }
        internal Sex Sex { get; }

        internal virtual void ToConsole()
        {
            Console.WriteLine("Human {0} {1}, age {2}", Name, Sex, Age);
        }
    }

    internal enum Sex
    {
        Man,
        Woman
    }
}
