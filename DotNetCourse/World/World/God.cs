using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Fabrics;
using World.Humans;

namespace World
{
    internal sealed class God : IGod
    {
        private Random rnd = new Random();
        private readonly List<IHumanFabric> fabrics = new List<IHumanFabric>();
        private List<Human> createdHumans = new List<Human>();
        internal God()
        {
            fabrics.Add(new ParentFabric());
            fabrics.Add(new CoolParentFabric());
            fabrics.Add(new StudentFabric());
            fabrics.Add(new BotanFabric());
        }
        public Human CreateHuman()
        {
            switch (createdHumans.Count)
            {
                case 0:
                    return CreateHuman(Sex.Man);
                case 1:
                    return CreateHuman(Sex.Woman);
                default:
                    return CreateHuman(Randomizer.GetRandomSex());
            }
        }

        public Human CreateHuman(Sex sex)
        {
            Human newHuman =  fabrics[rnd.Next(fabrics.Count)].CreateHuman(sex);
            createdHumans.Add(newHuman);
            return newHuman;
        }

        public Human CreatePair(Human human)
        {
            if (human == null)
            {
                throw new ArgumentNullException("null human");
            }
            Human newHuman;
            if (human is Botan)
            {
                newHuman = CoolParentFabric.CreatePair(human as Botan);
            }
            else if (human is CoolParent)
            {
                newHuman = BotanFabric.CreatePair(human as CoolParent);
            }
            else if (human is Student)
            {
                newHuman = ParentFabric.CreatePair(human as Student);
            }
            else if (human is Parent)
            {
                newHuman = StudentFabric.CreatePair(human as Parent);
            }
            else
            {
                throw new ArgumentException("Wrong human type");
            }
            createdHumans.Add(newHuman);
            return newHuman;
        }

        internal int this[int index]
        {
            get
            {
                if ((index < 0) || (index >= createdHumans.Count))
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (createdHumans[index] is CoolParent)
                {
                    return (createdHumans[index] as CoolParent).Money;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal int GetTotalMoney()
        {
            var size = createdHumans.Count;
            var totalMoney = 0;
            for (int i = 0; i < size; ++i)
            {
                totalMoney += this[i];
            }
            return totalMoney;
        }

        internal Human[] GetCreatedHumans()
        {
            return createdHumans.ToArray();
        }   
    }
}
