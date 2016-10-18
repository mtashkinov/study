using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Humans;

namespace World.Fabrics
{
    internal sealed class StudentFabric : IHumanFabric
    {
        private static Random rnd = new Random();
        public Human CreateHuman(Sex sex)
        {
            return new Student(Randomizer.GetRandomStudentAge(), NamesHelper.GenerateName(sex), sex,  NamesHelper.GeneratePatronymic(sex));
        }

        public static Student CreatePair(Parent parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("null parent");
            }
            Sex randomSex = Randomizer.GetRandomSex();

            return new Student(Randomizer.GetRandomStudentAge(), NamesHelper.GenerateName(randomSex), randomSex, NamesHelper.PatronymicFromName(randomSex, parent.Name));
        }
    }
}
