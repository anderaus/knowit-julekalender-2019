using System;

class Program
{
    static void Main(string[] args)
    {
        // var input = "oepHlpslainttnotePmseormoTtlst";
        var input = "tMlsioaplnKlflgiruKanliaebeLlkslikkpnerikTasatamkDpsdakeraBeIdaegptnuaKtmteorpuTaTtbtsesOHXxonibmksekaaoaKtrssegnveinRedlkkkroeekVtkekymmlooLnanoKtlstoepHrpeutdynfSneloietbol";
        Console.WriteLine($"Step #0: {input}");

        // Step #1
        var step1 = string.Empty;
        for (int i = input.Length; i > 0; i = i - 3)
        {
            step1 += input.Substring(i - 3, 3);
        }
        Console.WriteLine($"Step #1: {step1}");

        // Step #2
        var step2 = string.Empty;
        for (int i = 0; i <= step1.Length - 2; i = i + 2)
        {
            step2 += step1[i + 1];
            step2 += step1[i];
        }
        Console.WriteLine($"Step #2: {step2}");

        // Step #3
        var step3 = step2.Substring(step2.Length / 2) + step2.Substring(0, step2.Length / 2);
        Console.WriteLine($"Step #3: {step3}");
    }
}