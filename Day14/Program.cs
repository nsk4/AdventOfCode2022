using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    internal class Program
    {
        private enum SpotType
        {
            Start,
            Rock,
            Sand
        }

        static int GetSandAmount(Dictionary<(int, int), SpotType> spots, (int, int) startPoint, int lowestPoint)
        {
            int sandAmmount = 0;
            bool freefallReached = false;
            (int, int) currentPosition = startPoint;
            while (!freefallReached)
            {
                if (currentPosition.Item2 == lowestPoint)
                {
                    // Sand can only freefall from now
                    freefallReached = true;
                }
                else if (!spots.ContainsKey((currentPosition.Item1, currentPosition.Item2 + 1)))
                {
                    // Try moving sand down
                    currentPosition = (currentPosition.Item1, currentPosition.Item2 + 1);
                }
                else if (!spots.ContainsKey((currentPosition.Item1 - 1, currentPosition.Item2 + 1)))
                {
                    // Try moving sand down left
                    currentPosition = (currentPosition.Item1 - 1, currentPosition.Item2 + 1);
                }
                else if (!spots.ContainsKey((currentPosition.Item1 + 1, currentPosition.Item2 + 1)))
                {
                    // Try moving sand down right
                    currentPosition = (currentPosition.Item1 + 1, currentPosition.Item2 + 1);
                }
                else if (spots.ContainsKey((currentPosition.Item1, currentPosition.Item2)))
                {
                    // Cannot move sand at all
                    freefallReached = true;
                }
                else
                {
                    // Sand reached stable point
                    spots.Add((currentPosition.Item1, currentPosition.Item2), SpotType.Sand);
                    sandAmmount++;
                    currentPosition = startPoint;
                }
            }

            return sandAmmount;
        }

        static void Main(string[] args)
        {
            (int, int) startPoint = (500, 0);

            Dictionary<(int, int), SpotType> spots = new Dictionary<(int, int), SpotType>();
            spots.Add(startPoint, SpotType.Start);
            int lowestPoint = 0;

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] pointStrings = line.Split(new string[] {" -> "}, StringSplitOptions.None);
                int[] oldPoint = pointStrings[0].Split(',').Select(x => int.Parse(x)).ToArray();
                lowestPoint = Math.Max(lowestPoint, oldPoint[1]);
                for (int i=1; i< pointStrings.Length; i++)
                {
                    int[] newPoint = pointStrings[i].Split(',').Select(x => int.Parse(x)).ToArray();
                    if (oldPoint[1] == newPoint[1])
                    {
                        // Horizontal movement
                        for(int x = Math.Min(oldPoint[0], newPoint[0]); x <= Math.Max(oldPoint[0], newPoint[0]); x++)
                        {
                            if(!spots.ContainsKey((x, oldPoint[1])))
                            {
                                spots.Add((x, oldPoint[1]), SpotType.Rock);
                            }
                        }
                    }
                    else if (oldPoint[0] == newPoint[0])
                    {
                        // Vertical movement
                        for (int y = Math.Min(oldPoint[1], newPoint[1]); y <= Math.Max(oldPoint[1], newPoint[1]); y++)
                        {
                            if (!spots.ContainsKey((oldPoint[0], y)))
                            {
                                spots.Add((oldPoint[0], y), SpotType.Rock);
                            }
                        }

                        // Update lowest point
                        lowestPoint = Math.Max(lowestPoint, newPoint[1]);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }

                    oldPoint = newPoint;
                }
            }

            Console.WriteLine("Amount of sand: " + GetSandAmount(spots, startPoint, lowestPoint));

            spots.Aggregate(spot => spot.Value == SpotType.Sand);

            Console.ReadKey();
        }
    }
}
