using System;
using AdvancedWorld.HasName;

namespace AdvancedWorld.Fabrics
{
    internal sealed class NamesHelper
    {
        private static Random rnd = new Random();

        private static string[] manNames = new string[] { "Антон", "Борис", "Вадим", "Виктор", "Владимир", "Динар" };
        private static string[] womanNames = new string[] { "Анна", "Вера", "Даяна", "Инна", "Ирина", "Лара" };

        private const string manPatronymicAddition = "ович";
        private const string womanPatronymicAddition = "овна";
        private const int patronymicAdditionLength = 4;

        internal static string GenerateName(Sex sex)
        {
            switch (sex)
            {
                case Sex.Man:
                    return (string)manNames.GetValue(rnd.Next(manNames.Length));
                case Sex.Woman:
                    return (string)womanNames.GetValue(rnd.Next(womanNames.Length));
                default:
                    throw new NotSupportedException("Invalid sex");
            }

        }

        internal static string GeneratePatronymic(Sex sex)
        {
            switch (sex)
            {
                case Sex.Man:
                    return GenerateName(Sex.Man) + manPatronymicAddition;
                case Sex.Woman:
                    return GenerateName(Sex.Man) + womanPatronymicAddition;
                default:
                    throw new NotSupportedException("Invalid sex");
            }
        }

        internal static string PatronymicFromName(Sex sex, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid name");
            }
            switch (sex)
            {
                case Sex.Man:
                    return name + manPatronymicAddition;
                case Sex.Woman:
                    return name + womanPatronymicAddition;
                default:
                    throw new NotSupportedException("Invalid sex");
            }
        }

        internal static string NameFromPatronymic(Sex sex, string patronymic)
        {
            if ((patronymic == null) || (patronymic.Length <= 4))
            {
                throw new ArgumentException("Invalid patronymic");
            }

            string partToDel = patronymic.Substring(patronymic.Length - patronymicAdditionLength, patronymicAdditionLength);

            switch (sex)
            {
                case Sex.Man:
                    {
                        if (!partToDel.Equals(manPatronymicAddition))
                        {
                            throw new ArgumentException("Invalid patronymic name");
                        }
                        break;
                    }
                case Sex.Woman:
                    {
                        if (!partToDel.Equals(womanPatronymicAddition))
                        {
                            throw new ArgumentException("Invalid patronymic name");
                        }
                        break;
                    }
                default:
                    throw new NotSupportedException("Invalid sex");

            }

            return patronymic.Substring(0, patronymic.Length - patronymicAdditionLength);
        }
    }
}
