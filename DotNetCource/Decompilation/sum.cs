using System;

internal sealed class Programm
{
	static void Main()
	{
		System.Console.WriteLine("Я - интеллектуальный калькулятор!");
		System.Console.WriteLine("Как тебя зовут?");
		var name = System.Console.ReadLine();
		var rnd = new Random();
		var fst = rnd.Next(1, 11);
		var snd = rnd.Next(1, 11);
		System.Console.WriteLine("Сколько будет {0} + {1}?", fst, snd);
		var result = 0;
		var isInt = Int32.TryParse(Console.ReadLine().Trim(), out result);
		if (isInt && result == fst + snd)
		{
			System.Console.WriteLine("Верно, {0}!", name);
		} else
		{
			System.Console.WriteLine("{0}, ты не прав", name);
		}
	}
}