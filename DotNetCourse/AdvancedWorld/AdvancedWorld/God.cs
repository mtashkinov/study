using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedWorld.HasName;
using AdvancedWorld.Fabrics;
using System.Collections;
using System.Reflection;

namespace AdvancedWorld
{
    internal sealed class God : IGod
    {
        private const string hasNameNamespace = "AdvancedWorld.HasName.";

        private bool checkForLike = true;
        private Random rnd = new Random();
        private List<IHumanFabric> fabrics = new List<IHumanFabric>();

        internal God() : this(true)
        {
        }

        internal God(bool checkForLike)
        {
            this.checkForLike = checkForLike;
            fabrics.Add(new GirlFabric());
            fabrics.Add(new PrettyGirlFabric());
            fabrics.Add(new SmartGirlFabric());
            fabrics.Add(new StudentFabric());
            fabrics.Add(new BotanFabric());
        }

        public Human CreateHuman()
        {
            return fabrics[rnd.Next(fabrics.Count)].CreateHuman();
        }

        public IHasName MakeCouple(Human human1, Human human2)
        {
            if (human1.Sex == human2.Sex)
            {
                throw new WrongCoupleException();
            }
            Type human1Type = human1.GetType();
            Type human2Type = human2.GetType();
            CoupleAttribute attr1 = GetCorrectAttribute(human1Type, human2Type);
            if (IsLike(attr1.Probability))
            {
                CoupleAttribute attr2 = GetCorrectAttribute(human2Type, human1Type);
                if (IsLike(attr2.Probability))
                {
                    Human father = SelectFather(human1, human2);
                    Human mother = SelectMother(human1, human2);
                    return CreateCoupleResult(attr1.ChildType, mother.Name, father.Name);
                }
            }

            return null;
        }

        private Human SelectFather(Human human1, Human human2)
        {
            return human1.Sex == Sex.Man ? human1 : human2;
        }

        private Human SelectMother(Human human1, Human human2)
        {
            return human1.Sex == Sex.Woman ? human1 : human2;
        }

        private IHasName CreateCoupleResult(string typeName, string humanName, string fatherName)
        {
            Type type = Type.GetType(hasNameNamespace + typeName);
            IHasName hasName = (IHasName)Activator.CreateInstance(type, new object[1] { humanName });

            PropertyInfo info = type.GetProperty("Patronymic");
            if (info != null)
            {
                info.SetValue(hasName, NamesHelper.PatronymicFromName(((Human)hasName).Sex, fatherName));
            }

            return hasName;
        }

        private string GetNameFromType(Human human, Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    try
                    {
                        string value = propertyInfo.GetValue(human, null) as string;
                        return value;
                    } catch (ArgumentException ex)
                    {
                        throw ex;
                    }
                }
            }
            throw new ArgumentException("Human without string preperty");
        }

        private CoupleAttribute GetCorrectAttribute(Type type1, Type type2)
        {
            IEnumerator enumerator = Attribute.GetCustomAttributes(type1, false).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is CoupleAttribute)
                {
                    CoupleAttribute attr = enumerator.Current as CoupleAttribute;
                    if (attr.Pair.Equals(type2.Name))
                    {
                        return attr;
                    }
                }
            }

            throw new ArgumentException("Type without correct attribute");
        }

        private bool IsLike(double probability)
        {
            if (checkForLike)
            {
                return rnd.NextDouble() < probability;
            }
            else
            {
                return true;
            }
        }
    }
}
