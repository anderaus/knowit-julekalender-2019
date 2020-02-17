using System;
using System.IO;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        //var flattened = File.ReadAllText("eksempel.txt");
        var flattened = File.ReadAllText("img.txt");
        var length = flattened.Length;

        // Dimensions of output image is unknown, try all possible variations
        foreach (var ymax in GetPossibleYAxisLenghts(length))
        {
            var xmax = length / ymax;
            Console.WriteLine($"Creating image with dimensions ({xmax}, {ymax})");

            using (var image = new Image<Rgba32>(xmax, ymax))
            {
                for (var y = 0; y < ymax; y++)
                {
                    for (var x = 0; x < xmax; x++)
                    {
                        var flattenedFilePosition = (y * xmax) + x;
                        image[x, y] =
                            flattened[flattenedFilePosition] == '1'
                            ? Rgba32.Aquamarine
                            : Rgba32.Black;
                    }
                }

                // Then manually browse through all saved images until a good one is located (1287 x 560)
                image.Save($"img_{xmax}_{ymax}.png");
            }
        }
    }

    public static IEnumerable<int> GetPossibleYAxisLenghts(int area)
    {
        for (int i = area; i > 0; i--)
        {
            if (area % i == 0) yield return i;
        }
    }
}