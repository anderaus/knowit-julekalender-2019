using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        //var fjordLines = File.ReadAllLines("eksempel.txt");
        var fjordLines = File.ReadAllLines("fjord.txt");
        Location birteLocation = null;
        var birteLocations = new List<Location>();
        var numberOfLengths = 1;
        bool[,] fjord; fjord = new bool[fjordLines.First().Length, fjordLines.Count()];

        // Initial setup
        for (var y = 0; y < fjordLines.Count(); y++)
        {
            for (var x = 0; x < fjordLines.First().Length; x++)
            {
                var character = fjordLines.Skip(y).First().ToArray()[x];

                if (character == 'B')
                {
                    birteLocation = new Location(x, y);
                    birteLocations.Add(new Location(x, y));
                }
                else
                {
                    fjord[x, y] = character == '#';
                }
            }
        }

        // Game loop
        var directionIsUp = true;
        while (birteLocation.X < fjord.GetLength(0))
        {
            if (directionIsUp)
            {
                if (fjord[birteLocation.X, birteLocation.Y - 3])
                {
                    birteLocation.X++;
                    directionIsUp = false;
                    numberOfLengths++;
                }
                else
                {
                    birteLocation.X++;
                    birteLocation.Y--;
                }

            }
            else
            {
                if (fjord[birteLocation.X, birteLocation.Y + 3])
                {
                    birteLocation.X++;
                    directionIsUp = true;
                    numberOfLengths++;
                }
                else
                {
                    birteLocation.X++;
                    birteLocation.Y++;
                }
            }

            birteLocations.Add(new Location(birteLocation.X, birteLocation.Y));
        }

        Console.WriteLine("Number of lengths travelled: " + numberOfLengths);

        using (var image = new Image<Rgba32>(fjord.GetLength(0), fjord.GetLength(1)))
        {
            for (int y = 0; y < fjord.GetLength(1); y++)
            {
                for (int x = 0; x < fjord.GetLength(0); x++)
                {
                    if (birteLocations.Any(b => b.X == x && b.Y == y))
                    {
                        image[x, y] = Rgba32.Red;
                    }
                    else
                    {
                        image[x, y] = fjord[x, y] ? Rgba32.DarkGreen : Rgba32.LightBlue;
                    }
                }
            }

            image.Save("result.png");
        }

    }

    public class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }
}