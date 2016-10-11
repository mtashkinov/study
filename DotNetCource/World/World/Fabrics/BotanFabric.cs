using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Humans;

namespace World.Fabrics
{
    internal sealed class BotanFabric : IHumanFabric
    {
        private static Random rnd = new Random();
        public Human CreateHuman(Sex sex)
        {
            return new Botan(Randomizer.GetRandomStudentAge(), NamesHelper.GenerateName(sex), sex, NamesHelper.GeneratePatronymic(sex), GetRandomAvgMark());
        }

        public static Botan CreatePair(CoolParent parent)
        {
            if (parent == null)
            {
                throw new ArgumentException("Invalid parent");
            }
            Sex randomSex = Randomizer.GetRandomSex();

            return new Botan(Randomizer.GetRandomStudentAge(), NamesHelper.GenerateName(randomSex), randomSex, NamesHelper.PatronymicFromName(randomSex, parent.Name), Math.Log10(parent.Money));
        }

        private double GetRandomAvgMark()
        {
            return 3 + rnd.NextDouble() * 2;
        }
    }
}
