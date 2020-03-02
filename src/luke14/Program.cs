using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // var alphabet = new int[] { 2, 3 };
        var sequenceMaxLength = 217532235;
        var alphabet = new int[] { 2, 3, 5, 7, 11 };
        var sequence = new int[sequenceMaxLength];
        int numberOfAdds = 0;
        int sequencePointer = 0;

        sequence[0] = sequence[1] = 2;
        numberOfAdds++;
        sequencePointer++;

        var alphabetIndex = 0;

        while (sequencePointer <= sequenceMaxLength)
        {
            alphabetIndex++;
            if (alphabetIndex >= alphabet.Length) alphabetIndex = 0;

            for (int i = 1; i <= sequence[numberOfAdds]; i++)
            {
                sequencePointer++;
                if (sequencePointer >= sequenceMaxLength) break;
                sequence[sequencePointer] = alphabet[alphabetIndex];
            }
            numberOfAdds++;
        }

        var numberOfSevens = sequence.Count(s => s == 7);
        Console.WriteLine("Number of 7s: " + numberOfSevens);
        Console.WriteLine("ANSWER: " + 7 * numberOfSevens);
    }
}