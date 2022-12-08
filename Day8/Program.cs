using System;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int[,] grid = new int[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            #region Part 1 - Number of visible trees
            int[,] visibleGrid = new int[lines.Length, lines[0].Length];

            // Check from left and right
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int maxHeightLeft = -1;
                int maxHeightRight = -1;
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    // Check from left
                    if (grid[i, j] > maxHeightLeft)
                    {
                        visibleGrid[i, j] = 1;
                        maxHeightLeft = grid[i, j];
                    }

                    // Check from right
                    if (grid[i, grid.GetLength(1) - j - 1] > maxHeightRight)
                    {
                        visibleGrid[i, grid.GetLength(1) - j - 1] = 1;
                        maxHeightRight = grid[i, grid.GetLength(1) - j - 1];
                    }
                }
            }
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                int maxHeightTop = -1;
                int maxHeightBottom = -1;
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    // Check from top
                    if (grid[j, i] > maxHeightTop)
                    {
                        visibleGrid[j, i] = 1;
                        maxHeightTop = grid[j, i];
                    }

                    // Check from bottom
                    if (grid[grid.GetLength(0) - j - 1, i] > maxHeightBottom)
                    {
                        visibleGrid[grid.GetLength(0) - j - 1, i] = 1;
                        maxHeightBottom = grid[grid.GetLength(0) - j - 1, i];
                    }
                }
            }

            int visibleSum = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    visibleSum += visibleGrid[i, j];
                }
            }
            Console.WriteLine("Total number of visible trees: " + visibleSum);
            #endregion

            #region Part 2 - Highest visible tree score
            int[,] scoreGrid = new int[lines.Length, lines[0].Length];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    scoreGrid[i, j] = 1;

                    // Check right
                    int numberOfVisibleTrees = 0;
                    for (int x = j + 1; x < grid.GetLength(1); x++)
                    {
                        numberOfVisibleTrees++;
                        // Tree is tha same or bigger than the house tree
                        if (grid[i, x] >= grid[i, j])
                        {
                            break;
                        }
                    }
                    scoreGrid[i, j] *= numberOfVisibleTrees;

                    // Check left
                    numberOfVisibleTrees = 0;
                    for (int x = j - 1; x >= 0; x--)
                    {
                        numberOfVisibleTrees++;
                        // Tree is tha same or bigger than the house tree
                        if (grid[i, x] >= grid[i, j])
                        {
                            break;
                        }
                    }
                    scoreGrid[i, j] *= numberOfVisibleTrees;

                    // Check up
                    numberOfVisibleTrees = 0;
                    for (int x = i - 1; x >= 0; x--)
                    {
                        numberOfVisibleTrees++;
                        // Tree is tha same or bigger than the house tree
                        if (grid[x, j] >= grid[i, j])
                        {
                            break;
                        }
                    }
                    scoreGrid[i, j] *= numberOfVisibleTrees;

                    // Check down
                    numberOfVisibleTrees = 0;
                    for (int x = i + 1; x < grid.GetLength(0); x++)
                    {
                        numberOfVisibleTrees++;
                        // Tree is tha same or bigger than the house tree
                        if (grid[x, j] >= grid[i, j])
                        {
                            break;
                        }
                    }
                    scoreGrid[i, j] *= numberOfVisibleTrees;
                }
            }

            int maxScore = -1;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (scoreGrid[i, j] > maxScore)
                    {
                        maxScore = scoreGrid[i, j];
                    }
                }
            }

            Console.WriteLine("Max visible score is " + maxScore);
            #endregion

            Console.ReadLine();
        }
    }
}
