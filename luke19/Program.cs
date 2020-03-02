using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch watch = Stopwatch.StartNew();

        var sum =
            Enumerable
                .Range(1, 123454321)
                .AsParallel()
                .Where(x => IsHiddenPalindrome(x) && !IsPalindrome(x))
                .Sum(Convert.ToInt64);

        Console.WriteLine($"Sum: {sum}, time: {watch.Elapsed.TotalSeconds}");
    }

    static bool IsHiddenPalindrome(long number)
        => IsPalindrome(number + Convert.ToInt64(string.Join(string.Empty, number.ToString().Reverse())));

    static bool IsPalindrome(long number)
        => number.ToString().SequenceEqual(number.ToString().Reverse());
}