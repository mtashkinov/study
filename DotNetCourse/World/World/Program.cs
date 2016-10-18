using System;
using System.IO;
using System.Resources;

namespace World
{
    internal sealed class Program
    {
        private const string outputFile = "output.txt";
        public static void Main()
        {
            Console.WriteLine(Properties.Resources.Welcome);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(Properties.Resources.DoNotWorkOnSunday);
                return;
            }
            var isInt = true;
            var humansToCreate = 0;
            do
            {
                Console.WriteLine(Properties.Resources.HumansNum);
                isInt = Int32.TryParse(Console.ReadLine().Trim(), out humansToCreate);
                if (!isInt || (humansToCreate <= 0))
                {
                    Console.WriteLine(Properties.Resources.InvalidHumansNum);
                }
            } while (!isInt || (humansToCreate <= 0));
            
            
            God god = new God();
            Human[] createdHumans = GenerateHumans(god, humansToCreate);
            PrintHumans(createdHumans);

            Console.SetCursorPosition(0, Console.CursorTop - createdHumans.Length * 2 + 1);
            PrintPairs(GeneratePairs(god, createdHumans));
            SaveTotalMoney(god);
        }

        private static Human[] GenerateHumans(God god, int humansToCreate)
        {
            for (int i = 0; i < humansToCreate; ++i)
            {
                god.CreateHuman();
            }
            return god.GetCreatedHumans();
        }

        private static void PrintHumans(Human[] humans)
        {
            foreach (Human human in humans)
            {
                human.ToConsole();
                Console.WriteLine();
            }
        }

        private static void SaveTotalMoney(God god)
        {
            File.WriteAllText(outputFile, god.GetTotalMoney().ToString());
            Console.WriteLine(String.Format(Properties.Resources.MoneySaved, outputFile));
        }

        private static Human[] GeneratePairs(God god, Human[] humans)
        {
            Human[] pairs = new Human[humans.Length];
            for (int i = 0; i < humans.Length; ++i)
            {
                pairs[i] = god.CreatePair(humans[i]);
            }
            return pairs;
        }

        private static void PrintPairs(Human[] pairs)
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            foreach (Human pair in pairs)
            {
                pair.ToConsole();
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            Console.BackgroundColor = background;
        }
    }
}
