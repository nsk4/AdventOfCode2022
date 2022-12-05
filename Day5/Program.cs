using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            // Allocate columns
            int numberOfColumns = (lines[0].Length + 1) / 4;
            List<List<char>> originalColumns = new List<List<char>>();
            for (int i = 0; i < numberOfColumns; i++)
            {
                originalColumns.Add(new List<char>());
            }

            // Parse initial box positions
            int currentLine = 0;
            while (lines[currentLine].Length != 0)
            {
                // If current line is column numbering row then skip next 2 lines and break out.
                if (lines[currentLine].StartsWith(" 1 "))
                {
                    currentLine += 2;
                    break;
                }

                int boxNo = 0;
                for (int i = 0; i < lines[currentLine].Length; i += 4)
                {
                    string box = lines[currentLine].Substring(i, 3);
                    // Skip if columns is empty
                    if (box.Trim().Length == 0)
                    {
                        boxNo++;
                        continue;
                    }

                    originalColumns[boxNo].Add(box[1]);
                    boxNo++;
                }

                currentLine++;
            }

            #region Part 1 - iterative move
            // Copy columns
            List<List<char>> iterativeColumns = new List<List<char>>();
            originalColumns.ForEach(x => iterativeColumns.Add(x.ToList()));

            // Process commands
            for (int lineNum = currentLine; lineNum < lines.Length; lineNum++)
            {
                int[] commands = lines[lineNum].Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(' ').Select(x => int.Parse(x)).ToArray();

                // Iteratively move boxes
                for (int i = 0; i < commands[0]; i++)
                {
                    char el = iterativeColumns[commands[1] - 1][0];
                    iterativeColumns[commands[1] - 1].RemoveAt(0);
                    iterativeColumns[commands[2] - 1].Insert(0, el);
                }
            }

            // Construct word out of top boxes
            string iterativeWord = "";
            foreach (var column in iterativeColumns)
            {
                iterativeWord += column.First();
            }
            Console.WriteLine(iterativeWord);
            #endregion

            #region Part 2 - collective move
            // Copy columns
            List<List<char>> collectiveColumns = new List<List<char>>();
            originalColumns.ForEach(x => collectiveColumns.Add(x.ToList()));

            for (int lineNum = currentLine; lineNum < lines.Length; lineNum++)
            {
                int[] commands = lines[lineNum].Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(' ').Select(x => int.Parse(x)).ToArray();

                // Colectively move boxes
                collectiveColumns[commands[2] - 1].InsertRange(0, collectiveColumns[commands[1] - 1].Take(commands[0]).ToList()); ;
                collectiveColumns[commands[1] - 1].RemoveRange(0, commands[0]);
            }

            // Construct word out of top boxes
            string collectiveWord = "";
            foreach (var column in collectiveColumns)
            {
                collectiveWord += column.First();
            }
            Console.WriteLine(collectiveWord);
            #endregion

            Console.ReadLine();
        }
    }
}
