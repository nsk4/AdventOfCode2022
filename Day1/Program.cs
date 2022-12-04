using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> elfCals = new List<int>();

            int currentElfCals = 0;
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                if (line.Length == 0)
                {
                    elfCals.Add(currentElfCals);
                    currentElfCals = 0;
                    continue;
                }

                currentElfCals += int.Parse(line);
            }


            elfCals.Sort((a, b) => b.CompareTo(a));

            Console.WriteLine("Max cals: " + elfCals.First());

            int top3 = elfCals[0] + elfCals[1] + elfCals[2];

            Console.WriteLine("Top 3: " + top3);

            Console.ReadLine();

        }
    }
}
