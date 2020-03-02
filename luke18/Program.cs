using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Program
{
    static List<string> swFirstnamesMale = new List<string>();
    static List<string> swFirstnamesFemale = new List<string>();
    static List<string> swLastnameFirstParts = new List<string>();
    static List<string> swLastnameSecondParts = new List<string>();

    static void Main(string[] args)
    {
        // Prepare lookups
        var names = File.ReadAllLines("names.txt");
        int sectionCount = 0;
        foreach (var name in names)
        {
            if (name == "---")
            {
                sectionCount++;
                continue;
            }
            if (sectionCount == 0) swFirstnamesMale.Add(name);
            else if (sectionCount == 1) swFirstnamesFemale.Add(name);
            else if (sectionCount == 2) swLastnameFirstParts.Add(name);
            else if (sectionCount == 3) swLastnameSecondParts.Add(name);
        }

        // Read persons from file
        var employees = File.ReadAllLines("employees.csv");
        var starWarsNames = new List<string>();
        foreach (var employee in employees)
        {
            if (employee == "first_name,last_name,gender") continue;

            var employeeParts = employee.Split(',');
            var person = new Person
            {
                Firstname = employeeParts[0],
                Lastname = employeeParts[1],
                IsMale = employeeParts[2] == "M"
            };
            var starwarsname = GetStarWarsNameFor(person);
            starWarsNames.Add(starwarsname);
            // Console.WriteLine($"Person: {person} - Star Wars name: {starwarsname}");
        }

        var occurences = from name in starWarsNames
                         group name by name into grouped
                         orderby grouped.Count() ascending
                         select new { Name = grouped.Key, Occurances = grouped.Count() };
        foreach (var occurence in occurences)
        {
            Console.WriteLine($"{occurence.Name} ({occurence.Occurances})");
        }
    }

    private static string GetStarWarsNameFor(Person person)
    {
        // First name
        var firstNameindex = person.Firstname
            .Sum(c => (int)c) % (person.IsMale ? swFirstnamesMale.Count() : swFirstnamesFemale.Count());
        var firstName = person.IsMale ? swFirstnamesMale[firstNameindex] : swFirstnamesFemale[firstNameindex];

        // Last name
        var splitAtIndex = (int)Math.Ceiling((double)person.Lastname.Length / 2);
        var firstPartAlphabetIndexSum =
            person.Lastname
                .Substring(0, splitAtIndex)
                .ToLower()
                .Where(char.IsLetter)
                .Sum(s => (int)s - 96);
        var firstPartOfLastName = swLastnameFirstParts[firstPartAlphabetIndexSum % swLastnameFirstParts.Count()];

        var asciiMultiplied =
            person.Lastname
                .Substring(splitAtIndex)
                .Where(char.IsLetter)
                .Aggregate(1d, (product, next) => (long)(product * (int)next));
        var nextNumber = person.IsMale
            ? asciiMultiplied * person.Firstname.Length
            : asciiMultiplied * (person.Firstname.Length + person.Lastname.Length);
        var orderedNumber = long.Parse(String.Join("", nextNumber.ToString().OrderByDescending(x => x)));
        var lastPartOfLastName = $"{swLastnameSecondParts.ElementAt(Convert.ToInt32(orderedNumber % swLastnameSecondParts.Count()))}";

        return $"{firstName} {firstPartOfLastName}{lastPartOfLastName}";
    }
}

class Person
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool IsMale { get; set; }

}