using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("logg.txt");

        var days = new List<Day>();

        for (var i = 0; i < lines.Count(); i = i + 4)
        {
            var dateLine = lines[i];
            var day = new Day
            {
                Date = DateTime.ParseExact(dateLine + " 2018", "MMM dd':' yyyy", CultureInfo.GetCultureInfo("en-GB"))
            };

            AddComponentLine(day, lines[i + 1]);
            AddComponentLine(day, lines[i + 2]);
            AddComponentLine(day, lines[i + 3]);

            days.Add(day);
            Console.WriteLine($"Found day: {day}");
        }

        Console.WriteLine($"Sum tannkrem: {days.Sum(d => d.Tannkrem)} ml eller {days.Sum(d => d.Tannkrem) / 125} tuber");
        Console.WriteLine($"Sum sjampo: {days.Sum(d => d.Sjampo)} ml eller {days.Sum(d => d.Sjampo) / 300} flasker");
        Console.WriteLine($"Sum toalettpapir: {days.Sum(d => d.Toalettpapir)} meter eller {days.Sum(d => d.Toalettpapir) / 25} ruller");
        Console.WriteLine($"Sum sjampo brukt på søndager: {days.Where(d => d.Date.DayOfWeek == DayOfWeek.Sunday).Sum(d => d.Sjampo)} ml");
        Console.WriteLine($"Sum toalettpapir brukt på onsdager: {days.Where(d => d.Date.DayOfWeek == DayOfWeek.Wednesday).Sum(d => d.Toalettpapir)} meter");

        var answer =
            Math.Floor(days.Sum(d => d.Tannkrem) / 125) *
            Math.Floor(days.Sum(d => d.Sjampo) / 300) *
            Math.Floor(days.Sum(d => d.Toalettpapir) / 25) *
            days.Where(d => d.Date.DayOfWeek == DayOfWeek.Sunday).Sum(d => d.Sjampo) *
            days.Where(d => d.Date.DayOfWeek == DayOfWeek.Wednesday).Sum(d => d.Toalettpapir);

        Console.WriteLine("Løsning: " + answer);
    }

    private static void AddComponentLine(Day day, string line)
    {
        if (line.EndsWith("tannkrem"))
        {
            day.Tannkrem = double.Parse(TextBetween(line, "* ", " ml"));
        }
        if (line.EndsWith("sjampo"))
        {
            day.Sjampo = double.Parse(TextBetween(line, "* ", " ml"));
        }
        if (line.EndsWith("toalettpapir"))
        {
            day.Toalettpapir = double.Parse(TextBetween(line, "* ", " meter"));
        }
    }

    private static string TextBetween(string text, string start, string end)
    {
        int from = text.IndexOf(start) + start.Length;
        int to = text.LastIndexOf(end);
        return text.Substring(from, to - from);
    }
}

public class Day
{
    public DateTime Date { get; set; }
    public double Tannkrem { get; set; }
    public double Sjampo { get; set; }
    public double Toalettpapir { get; set; }

    public override string ToString()
    {
        return $"{Date} - Tannkrem: {Tannkrem} ml - Sjampo: {Sjampo} ml - Toalettpapir: {Toalettpapir} meter";
    }
}