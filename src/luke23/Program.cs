using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
class Program
{
    static void Main(string[] args)
    {
        var watch = Stopwatch.StartNew();

        var counter = 0;
        var primes = new List<int> { 2 };
        for (int i = 1; i <= 98765432; i++)
        {
            if (IsHarshadPrime(i, primes)) counter++;
        }

        watch.Stop();
        Console.WriteLine("Time spent: " + watch.Elapsed.TotalMilliseconds + " ms");
        Console.WriteLine("Count of Harshad primes: " + counter);
    }

    private static bool IsHarshadPrime(int number, List<int> primes)
    {
        var sumOfDigits = GetIntArray(number).Sum();
        var isHarshad = number % sumOfDigits == 0;
        return isHarshad ? IsPrime(sumOfDigits, primes) : false;
    }

    public static bool IsPrime(int number, List<int> primes)
    {
        if (primes.Contains(number)) return true;

        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        primes.Add(number);
        return true;
    }

    public static int[] GetIntArray(int num)
    {
        List<int> listOfInts = new List<int>();
        while (num > 0)
        {
            listOfInts.Add(num % 10);
            num = num / 10;
        }
        listOfInts.Reverse();
        return listOfInts.ToArray();
    }
}