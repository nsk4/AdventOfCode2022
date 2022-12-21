using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    internal class Program
    {
        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int CalculateDistance(Point point)
            {
                return Math.Abs(point.X - X) + Math.Abs(point.Y - Y);
            }
        }

        private class Link
        {
            public Point Sensor { get; set; }
            public Point Beacon { get; set; }

            public int Distance { get; set; }

            public Link(Point sensor, Point beacon)
            {
                this.Sensor = sensor;
                this.Beacon = beacon;
                Distance = this.Sensor.CalculateDistance(this.Beacon);
            }
        }

        static void Main(string[] args)
        {
            List<Link> links = new List<Link>();
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] splits = line.Split('=');
                links.Add(new Link(new Point(int.Parse(splits[1].Split(',')[0]), int.Parse(splits[2].Split(':')[0])),
                                   new Point(int.Parse(splits[3].Split(',')[0]), int.Parse(splits[4].Split(':')[0]))));
            }

            #region Part 1 - Row of interest
            const int rowOfInterest = 2000000;
            List<int> reachedPoints = new List<int>();
            List<int> rowOfInterestBeacons = new List<int>();
            foreach (var link in links)
            {
                // Calculate distance to beacon and to the row of interest. If row can be reached then add all points where beacon cannot be to the list.
                int distanceToInterest = link.Sensor.CalculateDistance(new Point(link.Sensor.X, rowOfInterest));
                int diff = link.Distance - distanceToInterest;
                if (diff >= 0)
                {
                    // Add all possible 
                    for (int i = link.Sensor.X - diff; i <= link.Sensor.X + diff; i++)
                    {
                        reachedPoints.Add(i);
                    }
                }
            }
            // Remove all points where beacon or sensor is present.
            reachedPoints.RemoveAll(x => links.Exists(y => y.Beacon.Y == rowOfInterest && x == y.Beacon.X || y.Sensor.Y == rowOfInterest && x == y.Sensor.X));
            Console.WriteLine("Impossible spots: " + reachedPoints.Distinct().Count());
            #endregion

            #region Part 2 - Possible point
            const int spaceSize = 4000000;
            bool found = true;
            for (int i = 0; i <= spaceSize; i++)
            {
                if (i % 100000 == 0)
                {
                    Console.WriteLine("i: " + i);
                }
                for (int j = 0; j <= spaceSize; j++)
                {
                    found = true;
                    foreach (var link in links)
                    {
                        int diff = link.Distance - link.Sensor.CalculateDistance(new Point(i, j));
                        if (diff >= 0)
                        {
                            found = false;
                            j = j + diff;
                            break;
                        }
                    }

                    if (found)
                    {
                        Console.WriteLine("Found " + i + " " + j);
                        long tuningFrequency = Convert.ToInt64(i) * 4000000L + Convert.ToInt64(j);
                        Console.WriteLine("Tuning frequency is: " + tuningFrequency);
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            #endregion

            Console.ReadLine();
        }
    }
}
