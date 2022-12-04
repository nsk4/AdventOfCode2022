using System;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfCompleteOverlappingPairs = 0;
            int numberOfPartialOverlappingPairs = 0;
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                int[] ranges = line.Split(',', '-').ToList().Select(x => int.Parse(x)).ToArray();

                // Calculate number of overlapping pairs
                if (ranges[0] >= ranges[2] && ranges[1] <= ranges[3] || // Elf 1 is contained in elf 2
                    ranges[2] >= ranges[0] && ranges[3] <= ranges[1])   // Elf 2 is contained in elf 1
                {
                    numberOfCompleteOverlappingPairs++;
                }


                // Calculate number of overlapping pairs
                if (ranges[0] >= ranges[2] && ranges[0] <= ranges[3] || // Elf 1 start overlap
                    ranges[1] >= ranges[2] && ranges[1] <= ranges[3] || // Elf 1 end overlap
                    ranges[2] >= ranges[0] && ranges[2] <= ranges[1] || // Elf 2 start overlap
                    ranges[3] >= ranges[0] && ranges[3] <= ranges[1])   // Elf 2 end overlap
                {
                    numberOfPartialOverlappingPairs++;
                }


                // numberOfOverlappingSections


            }

            Console.WriteLine("Number of complete pairs: " + numberOfCompleteOverlappingPairs);
            Console.WriteLine("Number of partial pairs: " + numberOfPartialOverlappingPairs);


            Console.ReadLine();
        }
    }
}
