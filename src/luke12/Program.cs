using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var used7IterationsCounter = 0;
        for (var i = 1000; i <= 9999; i++)
        {
            if (i % 1111 == 0)
            {
                continue;
            }

            var iterationsCounter = 0;
            var currentNumber = i;
            while (currentNumber != 6174)
            {
                iterationsCounter++;
                currentNumber = SubtractSmallestSortedRepresentation(currentNumber);

                if (iterationsCounter == 7)
                {
                    used7IterationsCounter++;
                }
            }
        }

        Console.WriteLine($"Result: {used7IterationsCounter}");
    }

    static int SubtractSmallestSortedRepresentation(int number)
    {
        int ascending = int.Parse(string.Concat(number.ToString("0000").OrderBy(c => c)));
        int descending = int.Parse(string.Concat(number.ToString("0000").OrderByDescending(c => c)));

        return ascending > descending
            ? ascending - descending
            : descending - ascending;
    }
}