using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        using (var image = Image.Load("mush.png").CloneAs<Rgba32>())
        {
            var span = image.GetPixelSpan();

            for (var i = span.Length - 1; i > 0; i--)
            {
                span[i].R = (byte)(span[i].R ^ span[i - 1].R);
                span[i].G = (byte)(span[i].G ^ span[i - 1].G);
                span[i].B = (byte)(span[i].B ^ span[i - 1].B);
            }

            image.Save("solution.png");
            Console.WriteLine("XOR'ed image saved");
        }
    }
}