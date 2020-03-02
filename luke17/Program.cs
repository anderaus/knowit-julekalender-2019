using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var hitsCounter = 0;

        foreach (var triangularNumber in Enumerable.Range(0, 1000000).Select(n => (long)n * (n + 1) / 2))
        {
            foreach (var rolledVersion in GetRolledVersions(triangularNumber))
            {
                if (IsSquare(rolledVersion))
                {
                    hitsCounter++;
                    Console.WriteLine(rolledVersion);
                    break;
                }
            }
        }

        Console.WriteLine($"Total hits: {hitsCounter}");
    }

    static IEnumerable<long> GetRolledVersions(long number)
    {
        var numberString = number.ToString();
        var numberOfDigits = numberString.Length;
        for (int i = 0; i <= numberOfDigits - 1; i++)
        {
            numberString = string.Concat(numberString.Substring(1), numberString.Substring(0, 1));
            yield return Convert.ToInt64(numberString);
        }
    }

    static bool IsSquare(long number)
    {
        return Math.Sqrt(number) % 1 == 0;
    }
}