using System;

namespace Day10
{
    internal class Program
    {
        static bool shouldCheckCycle(int cycle)
        {
            return (cycle - 20) % 40 == 0;
        }

        static void drawCrt(int cycle, int x)
        {
            if (cycle % 40 == 0)
            {
                Console.WriteLine();
            }
            if (x - 1 <= cycle % 40 && cycle % 40 <= x + 1)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }
        }

        static void Main(string[] args)
        {
            int cycle = 0;
            int x = 1;
            int signalStrengthSum = 0;
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                drawCrt(cycle, x);
                cycle++;
                if (shouldCheckCycle(cycle))
                {
                    signalStrengthSum += cycle * x;
                }

                string[] splits = line.Split(' ');
                if (splits[0].Equals("addx"))
                {
                    drawCrt(cycle, x);
                    cycle++;
                    if (shouldCheckCycle(cycle))
                    {
                        signalStrengthSum += cycle * x;
                    }
                    x += int.Parse(splits[1]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Signal strength sum: " + signalStrengthSum);

            Console.ReadLine();
        }
    }
}
