﻿using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld
{
    internal sealed class Program
    {
        internal static void Main(string[] args)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(Properties.Resources.SundayHoliday);
                return;
            }
            Console.WriteLine(Properties.Resources.Welcome);
            God god = new God();

            bool timeToExit = false;
            do
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                switch (keyinfo.Key)
                {
                    case ConsoleKey.Enter:
                        GenerateAndPrintCouple(god);
                        Console.WriteLine();
                        break;
                    case ConsoleKey.Q:
                    case ConsoleKey.F10:
                        timeToExit = true;
                        break;
                    default:
                        break;
                }
            } while (!timeToExit);
        }

        private static void GenerateAndPrintCouple(God god)
        {
            IHasName human1 = god.CreateHuman();
            IHasName human2 = god.CreateHuman();
            human1.ToConsole();
            human2.ToConsole();

            try
            {
                IHasName result = god.MakeCouple((Human)human1, (Human)human2);
                if (result == null)
                {
                    Console.WriteLine("They didn't like each other");
                }
                else
                {
                    result.ToConsole();
                }
            }
            catch (WrongCoupleException)
            {
                Console.WriteLine("Wrong couple");
            }
        }
    }
}
