using System;
using System.IO;

class Program
{
    static int[,] slime = new int[1000, 1000];
    static int x = 0;
    static int y = 0;
    static decimal time = 0;

    static void Main(string[] args)
    {
        var targets = File.ReadAllLines("coords.csv");
        //var targets = File.ReadAllLines("test.csv");

        foreach (var target in targets)
        {
            // Skip first line
            if (target == "# x,y") continue;

            var targetX = int.Parse(target.Split(',')[0]);
            var targetY = int.Parse(target.Split(',')[1]);

            // Move horizontally
            if (x != targetX)
            {
                while (x < targetX)
                {
                    slime[x, y]++;
                    x++;
                    time += slime[x, y] + 1;
                }
                while (x > targetX)
                {
                    slime[x, y]++;
                    x--;
                    time += slime[x, y] + 1;
                }
            }

            // Move vertically
            if (y != targetY)
            {
                while (y < targetY)
                {
                    slime[x, y]++;
                    y++;
                    time += slime[x, y] + 1;
                }
                while (y > targetY)
                {
                    slime[x, y]++;
                    y--;
                    time += slime[x, y] + 1;
                }
            }
        }

        Console.WriteLine($"Total time: {time} minutes");
    }
}