using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // EXAMPLE
        // var stripe = "IIGGFFAIISGIFFSGFFAAASS";
        // double speed = 300;

        // REAL
        var stripe = File.ReadAllText("terreng.txt");
        double speed = 10703437;

        var distance = 0;
        var iceInARow = 0;
        var previousWasMountain = false;

        for (int i = 0; i < stripe.Length; i++)
        {
            var terreng = stripe[i];

            if (terreng == 'G')
            {
                speed -= 27;
                previousWasMountain = false;
                iceInARow = 0;
            }
            else if (terreng == 'I')
            {
                iceInARow++;
                speed += 12 * iceInARow;
                previousWasMountain = false;
            }
            else if (terreng == 'A')
            {
                speed -= 59;
                previousWasMountain = false;
                iceInARow = 0;
            }
            else if (terreng == 'S')
            {
                speed -= 212;
                previousWasMountain = false;
                iceInARow = 0;
            }
            else if (terreng == 'F')
            {
                if (previousWasMountain)
                {
                    speed += 35;
                    previousWasMountain = false;
                }
                else
                {
                    speed -= 70;
                    previousWasMountain = true;
                }
                iceInARow = 0;
            }

            distance++;

            if (speed <= 0)
            {
                Console.WriteLine($"\nSleden stoppet!\nSluttdistanse: {distance} km");
                break;
            }
        }
    }
}