using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("krampus.txt");
        double krampusSum = 0;
        foreach (var line in lines)
        {
            var lineNumber = double.Parse(line);
            if (IsKrampus(lineNumber))
            {
                krampusSum += lineNumber;
            }
        }

        Console.WriteLine($"Krampussum: {krampusSum}");
    }

    static bool IsKrampus(double number)
    {
        var squaredString = (number * number).ToString();
        for (int i = 1; i < squaredString.Length; i++)
        {
            var left = double.Parse(squaredString.Substring(0, i));
            var right = double.Parse(squaredString.Substring(i));

            if (left != 0 && right != 0 && left + right == number)
            {
                Console.WriteLine($"*** {number} IS KRAMPUS! ***");
                return true;
            }
        }

        return false;
    }
}