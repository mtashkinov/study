using System;
using World.Humans;

namespace World.Fabrics
{
    internal sealed class ParentFabric : IHumanFabric
    {
        private static Random rnd = new Random();
        private const int maxChildsNum = 3;
        public Human CreateHuman(Sex sex)
        {
            return new Parent(Randomizer.GetRandomParentAge(), NamesHelper.GenerateName(Sex.Man), Sex.Man, rnd.Next(maxChildsNum));
        }

        public static Parent CreatePair(Student student)
        {
            if (student == null)
            {
                throw new ArgumentException("Invalid student");
            }
            if (student.Patronymic.Length < 5)
            {
                throw new ArgumentException("Too short patronymic name");
            }

            var name = NamesHelper.NameFromPatronymic(student.Sex, student.Patronymic);
            return new Parent(Randomizer.GetRandomParentAgeFromStudent(student.Age), name, Sex.Man, 1 + rnd.Next(maxChildsNum - 1)); // he surely has 1 child
        }
    }
}
