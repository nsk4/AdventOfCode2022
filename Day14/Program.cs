using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        static int GetSandAmount(Dictionary<(int, int), SpotType> spots, (int, int) startPoint, int lowestPoint, bool useFloor)
        {
            int sandAmmount = 0;
            bool limitReached = false;
            (int, int) currentPosition = startPoint;
            while (!limitReached)
            {
                if (!useFloor && currentPosition.Item2 == lowestPoint)
                {
                    // Sand can only freefall from now
                    limitReached = true;

                }
                else if (useFloor && currentPosition.Item2 + 1 == lowestPoint)
                {
                    // Next position is the cave floor
                    if (spots.ContainsKey((currentPosition.Item1, currentPosition.Item2)))
                    {
                        // Current spot is already taken.
                        limitReached = true;
                    }
                    else
                    {
                        // Current spot is the final rest point of the sand.
                        spots.Add((currentPosition.Item1, currentPosition.Item2), SpotType.Sand);
                        sandAmmount++;
                        currentPosition = startPoint;
                    }
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
                    // Sand cannot move anywhere, including its current position
                    if (spots[currentPosition].Equals(SpotType.Start))
                    {
                        sandAmmount++;
                    }
                    limitReached = true;
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

            Dictionary<(int, int), SpotType> freefallSpots = new Dictionary<(int, int), SpotType>();
            Dictionary<(int, int), SpotType> floorSpots = new Dictionary<(int, int), SpotType>();
            freefallSpots.Add(startPoint, SpotType.Start);
            floorSpots.Add(startPoint, SpotType.Start);
            int lowestPoint = 0;

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] pointStrings = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                int[] oldPoint = pointStrings[0].Split(',').Select(x => int.Parse(x)).ToArray();
                lowestPoint = Math.Max(lowestPoint, oldPoint[1]);
                for (int i = 1; i < pointStrings.Length; i++)
                {
                    int[] newPoint = pointStrings[i].Split(',').Select(x => int.Parse(x)).ToArray();
                    if (oldPoint[1] == newPoint[1])
                    {
                        // Horizontal movement
                        for (int x = Math.Min(oldPoint[0], newPoint[0]); x <= Math.Max(oldPoint[0], newPoint[0]); x++)
                        {
                            if (!freefallSpots.ContainsKey((x, oldPoint[1])))
                            {
                                freefallSpots.Add((x, oldPoint[1]), SpotType.Rock);
                                floorSpots.Add((x, oldPoint[1]), SpotType.Rock);
                            }
                        }
                    }
                    else if (oldPoint[0] == newPoint[0])
                    {
                        // Vertical movement
                        for (int y = Math.Min(oldPoint[1], newPoint[1]); y <= Math.Max(oldPoint[1], newPoint[1]); y++)
                        {
                            if (!freefallSpots.ContainsKey((oldPoint[0], y)))
                            {
                                freefallSpots.Add((oldPoint[0], y), SpotType.Rock);
                                floorSpots.Add((oldPoint[0], y), SpotType.Rock);
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

            Console.WriteLine("Freefall sand: " + GetSandAmount(freefallSpots, startPoint, lowestPoint, false));
            Console.WriteLine("Floor sand: " + GetSandAmount(floorSpots, startPoint, lowestPoint + 2, true));

            Console.ReadKey();
        }
    }
}
