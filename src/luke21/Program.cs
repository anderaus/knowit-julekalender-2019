using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("generations.txt");
        Console.WriteLine($"Got {lines.Length} generations");

        var generations = new List<Generation>();
        foreach (var line in lines)
        {
            var generation = new Generation
            {
                GenerationIndex = generations.Count()
            };
            foreach (var elfData in line.Split(';'))
            {
                generation.Elves.Add(new Elf
                {
                    Parent1Index = int.Parse(elfData.Split(',')[0]),
                    Parent2Index = int.Parse(elfData.Split(',')[1])
                });
            }
            generations.Add(generation);
        }

        var goalNumberOfOddetallKids = generations.First().Elves.Count() / 2;

        var startGeneration = generations.Skip(13).Take(1).Single();
        for (int i = 0; i < startGeneration.Elves.Count; i++)
        {
            Console.WriteLine("Working on generation " + startGeneration.GenerationIndex + " and elf " + i);
            var currentGeneration = startGeneration;
            var familyElves = new int[] { i };

            while (currentGeneration.GenerationIndex != generations.First().GenerationIndex)
            {
                var nextGeneration = generations.Single(g => g.GenerationIndex == currentGeneration.GenerationIndex - 1);
                var childrenInNextGeneration =
                    nextGeneration.Elves.Where(e => familyElves.Contains(e.Parent1Index) || familyElves.Contains(e.Parent2Index));
                familyElves = childrenInNextGeneration.Select(c => Array.IndexOf(nextGeneration.Elves.ToArray(), c)).ToArray();

                currentGeneration = nextGeneration;
            }

            var isReponsibleForOddeKidsCount = familyElves.Count(IsOdde);
            Console.WriteLine("Goal = " + goalNumberOfOddetallKids + ", Found = " + isReponsibleForOddeKidsCount);
            if (goalNumberOfOddetallKids == isReponsibleForOddeKidsCount)
            {
                Console.WriteLine("Success!");
                break;
            }
        }
    }

    public static bool IsOdde(int number)
    {
        return number % 2 != 0;
    }

    public class Generation
    {
        public int GenerationIndex;
        public List<Elf> Elves = new List<Elf>();
    }

    public class Elf
    {
        public int Parent1Index;
        public int Parent2Index;
    }
}