using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
class Program
{
    static void Main(string[] args)
    {
        var offset = 0;
        using (var image = new Image<Rgba32>(4800, 400))
        {
            foreach (var line in File.ReadAllLines("turer.txt"))
            {
                if (line == "---") offset += 110;
                else
                    image[
                        offset + Convert.ToInt32(line.Split(',')[0]),
                        380 - Convert.ToInt32(line.Split(',')[1])] = Rgba32.Red;
            }

            image.Save("result.png");
        }
    }
}