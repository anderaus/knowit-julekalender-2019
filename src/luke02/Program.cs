using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // var worldLines = File.ReadAllLines("world_test.txt");
        var worldLines = File.ReadAllLines("world.txt");

        double potentialReservoirSize = 0;
        bool previousPointWasEarth = false;
        double lineSum = 0;
        double totalSum = 0;

        foreach (var line in worldLines)
        {
            lineSum = 0;
            previousPointWasEarth = false;
            potentialReservoirSize = 0;

            foreach (char point in line)
            {
                if (point == ' ')
                {
                    if (previousPointWasEarth)
                    {
                        potentialReservoirSize = 1;
                    }
                    else if (potentialReservoirSize > 0)
                    {
                        potentialReservoirSize++;
                    }

                    // Set state for next round
                    previousPointWasEarth = false;
                }
                if (point == '#')
                {
                    // Found complete reservoir, sum it up!
                    if (potentialReservoirSize > 0)
                    {
                        lineSum += potentialReservoirSize;
                        potentialReservoirSize = 0;
                    }

                    // Set state for next round
                    previousPointWasEarth = true;
                }
            }
            Console.Write($"     LINESUM: {lineSum}");
            totalSum += lineSum;
            Console.WriteLine();
        }
        Console.WriteLine($"TOTAL SUM: {totalSum}");
    }
}