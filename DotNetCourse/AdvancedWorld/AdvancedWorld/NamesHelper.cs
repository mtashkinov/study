using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld
{
    internal sealed class NamesHelper
    {
        private static Random rnd = new Random();

        private static string[] manNames = new string[] { "Антон", "Борис", "Вадим", "Виктор", "Владимир", "Динар" };
        private static string[] womanNames = new string[] { "Анна", "Вера", "Даяна", "Инна", "Ирина", "Лара" };
        internal static string GenerateName(Sex sex)
        {
            switch (sex)
            {
                case Sex.Man:
                    return (string)manNames.GetValue(rnd.Next(manNames.Length));
                case Sex.Woman:
                    return (string)womanNames.GetValue(rnd.Next(womanNames.Length));
                default:
                    throw new ArgumentException("Invalid sex");
            }
            
        }

        internal static string GeneratePatronymic(Sex sex)
        {
            switch (sex)
            {
                case Sex.Man:
                    return GenerateName(Sex.Man) + "ович";
                case Sex.Woman:
                    return GenerateName(Sex.Man) + "овна";
                default:
                    throw new ArgumentException("Invalid sex");
            }
        }

        internal static string PatronymicFromName(Sex sex, string name)
        {
            if ((name == null) || (name.Length == 0))
            {
                throw new ArgumentException("Invalid name");
            }
            switch (sex)
            {
                case Sex.Man:
                    return name + "ович";
                case Sex.Woman:
                    return name + "овна";
                default:
                    return "";
            }
        }
    }
}
