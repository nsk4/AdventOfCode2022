using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    internal class Program
    {
        static void PrintGrid(List<(int, int)> spots)
        {
            // Print table
            int minX = spots.Min(x => x.Item1);
            int minY = spots.Min(x => x.Item2);
            int maxX = spots.Max(x => x.Item1);
            int maxY = spots.Max(x => x.Item2);
            for (int x = maxX; x >= minX; x--)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    bool found = false;
                    for (int q = 0; q < spots.Count; q++)
                    {
                        if (spots[q].Item1 == x && spots[q].Item2 == y)
                        {
                            found = true;
                            Console.Write(q);
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            #region Part 1 - Rope length 1
            (int, int) head = (0, 0);
            (int, int) tail = (0, 0);
            List<(int, int)> tailSpots = new List<(int, int)>();

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] splits = line.Split(' ').ToArray();
                for (int i = 0; i < int.Parse(splits[1]); i++)
                {

                    switch (splits[0])
                    {
                        case "L":
                            head = (head.Item1, --head.Item2);
                            break;
                        case "R":
                            head = (head.Item1, ++head.Item2);
                            break;
                        case "U":
                            head = (++head.Item1, head.Item2);
                            break;
                        case "D":
                            head = (--head.Item1, head.Item2);
                            break;
                        default:
                            break;
                    }

                    int h = head.Item2 - tail.Item2;
                    int v = head.Item1 - tail.Item1;

                    if (Math.Abs(h) + Math.Abs(v) > 2)
                    {
                        // Move tail diagonally
                        tail = (tail.Item1 + Math.Sign(v), tail.Item2 + Math.Sign(h));
                    }
                    else if (Math.Abs(h) > 1)
                    {
                        // Move tail horizontally
                        tail = (tail.Item1, tail.Item2 + Math.Sign(h));
                    }
                    else if (Math.Abs(v) > 1)
                    {
                        // Move tail vertically
                        tail = (tail.Item1 + Math.Sign(v), tail.Item2);
                    }

                    tailSpots.Add(tail);

                    //Console.WriteLine("Head: (" + head.Item1 + ", " + head.Item2 + "), tail: (" + tail.Item1 + ", " + tail.Item2 + ")");
                }
            }

            Console.WriteLine("Number of visited spots: " + tailSpots.Distinct().Count());
            #endregion

            #region Part 2 - Long rope
            List<(int, int)> rope = new List<(int, int)>();
            const int ropeLength = 10;
            for (int i = 0; i < ropeLength; i++)
            {
                rope.Add((0, 0));
            }
            List<(int, int)> visitedSpots = new List<(int, int)>();

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] splits = line.Split(' ').ToArray();
                for (int i = 0; i < int.Parse(splits[1]); i++)
                {

                    switch (splits[0])
                    {
                        case "L":
                            rope[0] = (rope[0].Item1, rope[0].Item2 - 1);
                            break;
                        case "R":
                            rope[0] = (rope[0].Item1, rope[0].Item2 + 1);
                            break;
                        case "U":
                            rope[0] = (rope[0].Item1 + 1, rope[0].Item2);
                            break;
                        case "D":
                            rope[0] = (rope[0].Item1 - 1, rope[0].Item2);
                            break;
                        default:
                            break;
                    }

                    // Update remaining elements of the rope
                    for (int j = 1; j < rope.Count; j++)
                    {
                        int h = rope[j - 1].Item2 - rope[j].Item2;
                        int v = rope[j - 1].Item1 - rope[j].Item1;

                        if (Math.Abs(h) + Math.Abs(v) > 2)
                        {
                            // Move tail diagonally
                            rope[j] = (rope[j].Item1 + Math.Sign(v), rope[j].Item2 + Math.Sign(h));
                        }
                        else if (Math.Abs(h) > 1)
                        {
                            // Move tail horizontally
                            rope[j] = (rope[j].Item1, rope[j].Item2 + Math.Sign(h));
                        }
                        else if (Math.Abs(v) > 1)
                        {
                            // Move tail vertically
                            rope[j] = (rope[j].Item1 + Math.Sign(v), rope[j].Item2);
                        }
                    }

                    visitedSpots.Add(rope.Last());
                }
            }

            Console.WriteLine("Number of visited spots: " + visitedSpots.Distinct().Count());
            #endregion

            Console.ReadLine();
        }
    }
}
