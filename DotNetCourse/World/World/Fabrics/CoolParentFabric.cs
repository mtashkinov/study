using System;
using World.Humans;

namespace World.Fabrics
{
    internal sealed class CoolParentFabric : IHumanFabric
    {
        private static Random rnd = new Random();
        private const int maxChildsNum = 3;
        private const int maxMoney = 100000;
        public Human CreateHuman(Sex sex)
        {
            return new CoolParent(Randomizer.GetRandomParentAge(), NamesHelper.GenerateName(Sex.Man), Sex.Man, rnd.Next(maxChildsNum), rnd.Next(maxMoney));
        }

        public static CoolParent CreatePair(Botan student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("null student");
            }
            if (student.Patronymic.Length < 5)
            {
                throw new ArgumentException("Too short patronymic name");
            }

            var name = NamesHelper.NameFromPatronymic(student.Sex, student.Patronymic);
            return new CoolParent(Randomizer.GetRandomParentAgeFromStudent(student.Age), name, Sex.Man, 1 + rnd.Next(maxChildsNum - 1), MoneyHelper.MarkToMoney(student.AverageMark)); // he surely has 1 child
        }
    }
}
