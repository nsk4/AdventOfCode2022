using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        static private int GetLetterPriority(char letter)
        {
            if (letter >= 'a' && letter <= 'z')
            {
                return letter - 'a' + 1;
            }
            if (letter >= 'A' && letter <= 'Z')
            {
                return letter - 'A' + 27;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        static void Main(string[] args)
        {
            #region Part 1 - Calculate priorities
            int prioritySum = 0;
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string comp1 = line.Substring(0, line.Length / 2);
                string comp2 = line.Substring(line.Length / 2);

                // Get duplicates
                List<char> duplicates = new List<char>();
                foreach (char letter1 in comp1)
                {
                    foreach (char letter2 in comp2)
                    {
                        if (letter1.Equals(letter2))
                        {
                            duplicates.Add(letter1);
                        }
                    }
                }

                // Calculate priorities
                foreach (char letter in duplicates.Distinct().ToList())
                {
                    prioritySum += GetLetterPriority(letter);
                }

            }

            Console.WriteLine("Priority sum: " + prioritySum);
            #endregion

            #region Part 2 - Calculate badges
            int badgeSum = 0;
            string[] lines = System.IO.File.ReadLines("input.txt").ToArray();
            for (var i = 0; i < lines.Length; i += 3)
            {
                bool found = false;
                foreach (char letter1 in lines[i])
                {
                    foreach (char letter2 in lines[i + 1])
                    {
                        if (letter1.Equals(letter2))
                        {
                            foreach (char letter3 in lines[i + 2])
                            {
                                if (letter1.Equals(letter3))
                                {
                                    // Found badge, calc priority and break out of all loops
                                    badgeSum += GetLetterPriority(letter1);
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Badge sum: " + badgeSum);

            #endregion

            Console.ReadLine();
        }
    }
}
