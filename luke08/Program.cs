using System;
using System.Linq;

public class Program
{
    static double points;
    static Wheel[] wheels = new[] {
            new Wheel(new Func<double, double>[] { PLUSS101, OPP7, MINUS9, PLUSS101 }),
            new Wheel(new Func<double, double>[] { TREKK1FRAODDE, MINUS1, MINUS9, PLUSS1TILPAR }),
            new Wheel(new Func<double, double>[] { PLUSS1TILPAR, PLUSS4, PLUSS101, MINUS9 }),
            new Wheel(new Func<double, double>[] { MINUS9, PLUSS101, TREKK1FRAODDE, MINUS1 }),
            new Wheel(new Func<double, double>[] { ROTERODDE, MINUS1, PLUSS4, ROTERALLE }),
            new Wheel(new Func<double, double>[] { GANGEMSD, PLUSS4, MINUS9, STOPP }),
            new Wheel(new Func<double, double>[] { MINUS1, PLUSS4, MINUS9, PLUSS101 }),
            new Wheel(new Func<double, double>[] { PLUSS1TILPAR, MINUS9, TREKK1FRAODDE, DELEMSD }),
            new Wheel(new Func<double, double>[] { PLUSS101, REVERSERSIFFER, MINUS1, ROTERPAR }),
            new Wheel(new Func<double, double>[] { PLUSS4, GANGEMSD, REVERSERSIFFER, MINUS9 })
        };

    static void Main(string[] args)
    {
        for (int i = 1; i < 10; i++)
        {
            points = i;
            // GAMELOOP
            var doLoop = true;
            while (doLoop)
            {
                var wheelNumber = int.Parse(points.ToString().Last().ToString());
                var currentWheel = wheels[wheelNumber];
                var operation = currentWheel.Operations[currentWheel.Position];
                currentWheel.Rotate();

                // Console.Write($"Wheel: #{wheelNumber} - {operation.Method} - Points before: {points} - ");
                points = operation(points);
                // Console.WriteLine($"Points after: {points}");
                if (points == -666666)
                {
                    doLoop = false;
                }
            }

            // Reset and be ready for next round
            foreach (var wheel in wheels)
            {
                wheel.Position = 0;
            }
        }
    }

    static double STOPP(double input)
    {
        Console.WriteLine($"FINAL POINTS: {points}");
        // System.Environment.Exit(1);
        return -666666;
    }

    static double ROTERODDE(double input)
    {
        for (int i = 1; i <= 9; i = i + 2)
        {
            wheels[i].Rotate();
        }

        return input;
    }

    static double ROTERPAR(double input)
    {
        for (int i = 0; i <= 9; i = i + 2)
        {
            wheels[i].Rotate();
        }

        return input;
    }

    static double ROTERALLE(double input)
    {
        foreach (var wheel in wheels)
        {
            wheel.Rotate();
        }

        return input;
    }

    static double TREKK1FRAODDE(double input)
    {
        var isNegative = input < 0;
        var inputString = Math.Abs(input).ToString();
        var outputString = "";
        foreach (char letter in inputString)
        {
            if (int.Parse(letter.ToString()) % 2 != 0)
            {
                outputString += int.Parse(letter.ToString()) - 1;
            }
            else
            {
                outputString += letter;
            }
        }
        return isNegative ? -double.Parse(outputString) : double.Parse(outputString);
    }

    static double PLUSS1TILPAR(double input)
    {
        var isNegative = input < 0;
        var inputString = Math.Abs(input).ToString();
        var outputString = "";
        foreach (char letter in inputString)
        {
            if (int.Parse(letter.ToString()) % 2 == 0)
            {
                outputString += int.Parse(letter.ToString()) + 1;
            }
            else
            {
                outputString += letter;
            }
        }
        return isNegative ? -double.Parse(outputString) : double.Parse(outputString);
    }

    static double DELEMSD(double input)
    {
        var div = double.Parse(Math.Abs(input).ToString().First().ToString());
        var remainder = input % div;
        var dived = Math.Floor(input / div);
        return dived;
    }

    static double GANGEMSD(double input)
    {
        var mult = double.Parse(Math.Abs(input).ToString().First().ToString());
        return input * mult;
    }

    static double OPP7(double input)
    {
        while (input.ToString().Last() != '7')
        {
            input++;
        }
        return input;
    }

    static double PLUSS101(double input) => input + 101;

    static double MINUS9(double input) => input - 9;

    static double MINUS1(double input) => input - 1;

    private static double PLUSS4(double input) => input + 4;

    static double REVERSERSIFFER(double input)
    {
        var isNegative = input < 0;
        var inputString = Math.Abs(input).ToString();
        var reversed = double.Parse(inputString.Reverse().ToArray());
        return isNegative ? reversed * -1 : reversed;
    }
}

public class Wheel
{
    public int Position { get; set; }
    public Func<double, double>[] Operations { get; set; }

    public Wheel(Func<double, double>[] operations)
    {
        Operations = operations;
    }

    public void Rotate()
    {
        Position++;
        if (Position == 4) Position = 0;
    }
}