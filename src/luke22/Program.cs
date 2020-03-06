using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var forestLines = File.ReadAllLines("forest.txt");
        var forest = new bool[forestLines.First().Length, forestLines.Length];

        int x = 0;
        int y = 0;
        foreach (var forestLine in forestLines)
        {
            foreach (var forestPiece in forestLine)
            {
                forest[x++, y] = forestPiece == '#';

            }
            x = 0;
            y++;
        }

        double sumOfHeights = 0;

        for (int x2 = 0; x2 < forest.GetLength(0); x2++)
        {
            // Hvis er stamme
            if (forest[x2, 34])
            {
                Console.WriteLine("Found stamme at (x,y) = (" + x2 + "," + 34 + ")");
                var y2 = 34;
                while (y2 >= 0 && forest[x2, y2])
                {
                    sumOfHeights++;
                    y2--;
                }
                Console.WriteLine("SumOfHeights: " + sumOfHeights);
            }
        }

        Console.WriteLine("Result (40 kr per height unit): " + sumOfHeights * 40);
    }
}