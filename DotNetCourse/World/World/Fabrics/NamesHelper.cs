using System;

namespace World.Fabrics
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

        internal static string NameFromPatronymic(Sex sex, string patronymic)
        {
            string partToDel = patronymic.Substring(patronymic.Length - 4, 4);

            switch (sex)
            {
                case Sex.Man:
                    {
                        if (!partToDel.Equals("ович"))
                        {
                            throw new ArgumentException("Invalid patronymic name");
                        }
                        break;
                    }
                case Sex.Woman:
                    {
                        if (!partToDel.Equals("овна"))
                        {
                            throw new ArgumentException("Invalid patronymic name");
                        }
                        break;
                    }
            }

            return patronymic.Substring(0, patronymic.Length - 4);
        }
    }
}
