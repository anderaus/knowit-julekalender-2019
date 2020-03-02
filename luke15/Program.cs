using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    public static CultureInfo nbNO = new CultureInfo("nb-NO")
    {
        NumberFormat = { NumberDecimalSeparator = "." }
    };

    static void Main(string[] args)
    {
        var mesh = new List<Triangle>();
        foreach (var line in File.ReadAllLines("model.csv"))
        {
            Console.WriteLine("Line: " + line);
            var lineParts = line.Split(',');
            mesh.Add(new Triangle
            {
                P1 = new Vector(ParseDec(lineParts[0]), ParseDec(lineParts[1]), ParseDec(lineParts[2])),
                P2 = new Vector(ParseDec(lineParts[3]), ParseDec(lineParts[4]), ParseDec(lineParts[5])),
                P3 = new Vector(ParseDec(lineParts[6]), ParseDec(lineParts[7]), ParseDec(lineParts[8])),
            });
        }
        Console.WriteLine("Got " + mesh.Count() + " triangles!");

        var volumes = from t in mesh
                      select SignedVolumeOfTriangle(t.P1, t.P2, t.P3);

        Console.WriteLine("Result: " + Math.Abs(volumes.Sum()) / 1000 + " ml");
    }

    public static decimal ParseDec(string input)
    {
        return decimal.Parse(input, NumberStyles.Any, nbNO);
    }

    public static decimal SignedVolumeOfTriangle(Vector p1, Vector p2, Vector p3)
    {
        var v321 = p3.X * p2.Y * p1.Z;
        var v231 = p2.X * p3.Y * p1.Z;
        var v312 = p3.X * p1.Y * p2.Z;
        var v132 = p1.X * p3.Y * p2.Z;
        var v213 = p2.X * p1.Y * p3.Z;
        var v123 = p1.X * p2.Y * p3.Z;
        return (1.0m / 6.0m) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    public class Triangle
    {
        public Vector P1;
        public Vector P2;
        public Vector P3;
    }

    public class Vector
    {
        public Vector(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public decimal X { get; }
        public decimal Y { get; }
        public decimal Z { get; }
    }
}