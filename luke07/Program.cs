using System;

class Program
{
    static void Main(string[] args)
    {
        for (int day = 1; day < 7; day++)
        {
            var x = day + 1;
            Console.WriteLine($"x = {x}");

            // Step #1
            double y = 0;
            for (double ym = 2; ym <= 27644436; ym++)
            {
                var b = ym * x;
                var r = b % 27644437;
                if (r == 1)
                {
                    y = ym;
                    continue;
                }
            }
            Console.WriteLine($"y = {y}");

            // Step #2
            double z = 5897 * y;
            Console.WriteLine($"z = {z}");

            // Step #3
            double code = z % 27644437;
            Console.WriteLine($"Code: {code}");
        }
    }
}