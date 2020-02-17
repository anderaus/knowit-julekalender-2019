using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int dragonSize = 50;
        int numberOfSheep = 0;
        int day = 1;
        int sequentialUnderfedDays = 0;

        var sheepAddedPerDay = File.ReadAllText("sau.txt").Split(',').Select(int.Parse);

        foreach (var addedSheep in sheepAddedPerDay)
        {
            numberOfSheep += addedSheep;
            Console.Write($"Day #{day.ToString("000")}. Dragonsize: {dragonSize.ToString("000")}. SheepAtStartOfDay: {numberOfSheep.ToString("000")} ");

            if (dragonSize <= numberOfSheep)
            {
                numberOfSheep -= dragonSize;
                dragonSize++;
                sequentialUnderfedDays = 0;
            }
            else
            {
                numberOfSheep = 0;
                dragonSize--;
                sequentialUnderfedDays++;

                Console.Write($"Underfed! {sequentialUnderfedDays} times in a row");
                if (sequentialUnderfedDays >= 5)
                {
                    Console.Write($"\nTHE DRAGON WENT BERZERK ON DAY #{day.ToString("000")}. THE ANSWER IS {day - 1}");
                    return;
                }
            }

            day++;
            Console.WriteLine();
        }
    }
}