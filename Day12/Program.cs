using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    internal class Program
    {
        enum Direction
        {
            None = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4
        }
        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Cost { get; set; }
            public Direction MoveDirection { get; set; }

            public Point(int x, int y, int height)
            {
                this.X = x;
                this.Y = y;
                this.Height = height;
                this.Cost = int.MaxValue;
                this.MoveDirection = Direction.None;
            }
        }

        static int GetMinimumCost(string[] lines, bool lookForStart)
        {
            Point[,] grid = new Point[lines.Length, lines[0].Length];
            Point startPosition = null;
            Point endPosition = null;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'S')
                    {
                        // Start position
                        Point gridPoint = new Point(i, j, 0);
                        grid[i, j] = gridPoint;
                        startPosition = gridPoint;
                    }
                    else if (lines[i][j] == 'E')
                    {
                        Point gridPoint = new Point(i, j, 'z' - 'a');
                        grid[i, j] = gridPoint;
                        gridPoint.Cost = 0;
                        endPosition = gridPoint;
                    }
                    else
                    {
                        Point gridPoint = new Point(i, j, lines[i][j] - 'a');
                        grid[i, j] = gridPoint;
                    }
                }
            }

            // Run algorithm
            List<Point> pointsToVisit = new List<Point>
            {
                grid[endPosition.X, endPosition.Y]
            };

            while (pointsToVisit.Count > 0)
            {
                Point currentPoint = pointsToVisit.First();
                pointsToVisit.RemoveAt(0);

                if (lookForStart)
                {
                    if (currentPoint.X == startPosition.X && currentPoint.Y == startPosition.Y)
                    {
                        // Found start, don't do anything
                        Console.WriteLine("Reached start");
                        continue;
                    }
                }
                else if (currentPoint.Height == 0)
                {
                    // Found lowest point, don't do anything
                    Console.WriteLine("Reached lowest point");
                    continue;
                }

                // Try moving up
                if (currentPoint.X > 0)
                {
                    Point visitPoint = grid[currentPoint.X - 1, currentPoint.Y];
                    if (currentPoint.Height - visitPoint.Height <= 1 && visitPoint.Cost > currentPoint.Cost + 1)
                    {
                        visitPoint.Cost = currentPoint.Cost + 1;
                        visitPoint.MoveDirection = Direction.Down;
                        pointsToVisit.Add(visitPoint);
                    }
                }

                // Try moving down
                if (currentPoint.X < grid.GetLength(0) - 1)
                {
                    Point visitPoint = grid[currentPoint.X + 1, currentPoint.Y];
                    if (currentPoint.Height - visitPoint.Height <= 1 && visitPoint.Cost > currentPoint.Cost + 1)
                    {
                        visitPoint.Cost = currentPoint.Cost + 1;
                        visitPoint.MoveDirection = Direction.Up;
                        pointsToVisit.Add(visitPoint);
                    }
                }

                // Try moving left
                if (currentPoint.Y > 0)
                {
                    Point visitPoint = grid[currentPoint.X, currentPoint.Y - 1];
                    if (currentPoint.Height - visitPoint.Height <= 1 && visitPoint.Cost > currentPoint.Cost + 1)
                    {
                        visitPoint.Cost = currentPoint.Cost + 1;
                        visitPoint.MoveDirection = Direction.Right;
                        pointsToVisit.Add(visitPoint);
                    }
                }

                // Try moving right
                if (currentPoint.Y < grid.GetLength(1) - 1)
                {
                    Point visitPoint = grid[currentPoint.X, currentPoint.Y + 1];
                    if (currentPoint.Height - visitPoint.Height <= 1 && visitPoint.Cost > currentPoint.Cost + 1)
                    {
                        visitPoint.Cost = currentPoint.Cost + 1;
                        visitPoint.MoveDirection = Direction.Left;
                        pointsToVisit.Add(visitPoint);
                    }
                }
            }

            if (lookForStart)
            {
                return startPosition.Cost;
            }
            else
            {
                int minCost = int.MaxValue;
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j].Height == 0 && grid[i, j].Cost < minCost)
                        {
                            minCost = grid[i, j].Cost;
                        }
                    }
                }
                return minCost;
            }
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            Console.WriteLine("Cost from start to end: " + GetMinimumCost(lines, true));
            Console.WriteLine("Cost from lowest to end: " + GetMinimumCost(lines, false));

            Console.ReadLine();
        }
    }
}
