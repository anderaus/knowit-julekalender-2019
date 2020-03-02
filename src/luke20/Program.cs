using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var isClockwise = true;
        var elves = new int[] { 0, 0, 0, 0, 0 };
        var currentElfIndex = 0;
        var step = 1;

        while (step <= 1000740)
        {
            elves[currentElfIndex]++;
            step++;

            if (elves.Count(e => e == elves.Min()) == 1 && IsPrime(step))
            {
                currentElfIndex = Array.FindIndex(elves, 0, elves.Length, e => e == elves.Min());
            }
            else if (step % 28 == 0)
            {
                isClockwise = !isClockwise;
                currentElfIndex = GetNextElfIndex(currentElfIndex, isClockwise);
            }
            else if (step % 2 == 0
                && elves.Max() == elves[GetNextElfIndex(currentElfIndex, isClockwise)]
                && elves.Count(e => e == elves.Max()) == 1)
            {
                currentElfIndex = GetNextElfIndex(GetNextElfIndex(currentElfIndex, isClockwise), isClockwise);
            }
            else if (step % 7 == 0)
            {
                currentElfIndex = 4;
            }
            else
            {
                currentElfIndex = GetNextElfIndex(currentElfIndex, isClockwise);
            }
        }

        Console.WriteLine($"Elf work loads: {string.Join(',', elves)}");
        Console.WriteLine($"Highest performer vs laziest elf diff: {elves.Max() - elves.Min()}");
    }

    private static int GetNextElfIndex(int currentElfIndex, bool isClockwise)
    {
        var newIndex = currentElfIndex += isClockwise ? 1 : -1;
        if (newIndex == -1) newIndex = 4;
        if (newIndex == 5) newIndex = 0;
        return newIndex;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }
}