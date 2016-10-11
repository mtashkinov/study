﻿using System;
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
            return fabrics[rnd.Next(fabrics.Count())].CreateHuman();
        }

        public IHasName MakeCouple(Human human1, Human human2)
        {
            if (human1.Sex == human2.Sex)
            {
                throw new WrongCoupleException();
            }
            Type human1Type = DetermineHumanType(human1);
            Type human2Type = DetermineHumanType(human2);
            CoupleAttribute attr1 = GetCorrectAttribute(human1Type, human2Type);
            if (isLike(attr1.Probability))
            {
                CoupleAttribute attr2 = GetCorrectAttribute(human2Type, human1Type);
                if (isLike(attr2.Probability))
                {
                    Human father = GetFather(human1, human2);
                    Human mother = GetMother(human1, human2);
                    return CreateCoupleResult(attr1.ChildType, mother.Name, father.Name);
                }
            }

            return null;
        }

        private Human GetFather(Human human1, Human human2)
        {
            return human1.Sex == Sex.Man ? human1 : human2;
        }

        private Human GetMother(Human human1, Human human2)
        {
            return human1.Sex == Sex.Woman ? human1 : human2;
        }

        private IHasName CreateCoupleResult(string typeName, string humanName, string fatherName)
        {
            Type type = Type.GetType("AdvancedWorld.HasName." + typeName);
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

        private bool isLike(double probability)
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

        private Type DetermineHumanType(Human human)
        {
            if (human is Botan)
            {
                return typeof(Botan);
            } else if (human is Student)
            {
                return typeof(Student);
            } else if (human is SmartGirl)
            {
                return typeof(SmartGirl);
            } else if (human is PrettyGirl)
            {
                return typeof(PrettyGirl);
            } else if (human is Girl)
            {
                return typeof(Girl);
            }
            else throw new ArgumentException("Wrong human type");
        }
    }
}
