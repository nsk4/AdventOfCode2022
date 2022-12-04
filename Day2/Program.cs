using System;
using System.Collections.Generic;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        static private int NpcActionToNumber(string action)
        {
            if (action.Equals("A")) { return 0; }
            if (action.Equals("B")) { return 1; }
            if (action.Equals("C")) { return 2; }
            throw new System.ArgumentException();
        }

        static private int PlayerActionToNumber(string action, string m1, string m2, string m3)
        {
            if (action.Equals(m1))
            {
                return 0;
            }
            if (action.Equals(m2))
            {
                return 1;
            }
            if (action.Equals(m3))
            {
                return 2;
            }
            throw new System.ArgumentException();
        }

        static void Main(string[] args)
        {
            List<(string, string)> plays = new List<(string, string)>();

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] actions = line.Split(' ');
                plays.Add((actions[0], actions[1]));
            }

            int[,] mappingTable = new int[,] { // R P S
                                               {3+1, 0+1, 6+1 },
                                               {6+2, 3+2, 0+2 },
                                               {0+3, 6+3, 3+3 }};

            #region Part 1
            // Finding most optimal strategy
            int[] scores = new int[6];
            foreach (var play in plays)
            {
                scores[0] += mappingTable[PlayerActionToNumber(play.Item2, "X", "Y", "Z"), NpcActionToNumber(play.Item1)];
                scores[1] += mappingTable[PlayerActionToNumber(play.Item2, "X", "Z", "Y"), NpcActionToNumber(play.Item1)];


                scores[2] += mappingTable[PlayerActionToNumber(play.Item2, "Y", "X", "Z"), NpcActionToNumber(play.Item1)];
                scores[3] += mappingTable[PlayerActionToNumber(play.Item2, "Y", "Z", "X"), NpcActionToNumber(play.Item1)];

                scores[4] += mappingTable[PlayerActionToNumber(play.Item2, "Z", "X", "Y"), NpcActionToNumber(play.Item1)];
                scores[5] += mappingTable[PlayerActionToNumber(play.Item2, "Z", "Y", "X"), NpcActionToNumber(play.Item1)];
                Console.WriteLine(string.Join(", ", scores.ToArray()));
            }

            // Only 1st strategy matters...confusing instructions
            Console.WriteLine("Final score: " + scores[0]);
            #endregion

            #region Part 2
            int[,] simplifiedMappingTable = new int[,] { { 3, 1, 2 }, // Lose
                                                         { 1+3, 2+3, 3+3 }, // Draw
                                                         { 2+6, 3+6, 1+6 }}; // Win
            int score = 0;
            foreach (var play in plays)
            {
                if (play.Item2.Equals("X"))
                {
                    // Lose
                    score += simplifiedMappingTable[0, NpcActionToNumber(play.Item1)];
                }
                if (play.Item2.Equals("Y"))
                {
                    // Draw
                    score += simplifiedMappingTable[1, NpcActionToNumber(play.Item1)];
                }
                if (play.Item2.Equals("Z"))
                {
                    // Win
                    score += simplifiedMappingTable[2, NpcActionToNumber(play.Item1)];
                }

                Console.WriteLine(score);
            }

            Console.WriteLine("Final score: " + score);
            #endregion

            Console.ReadLine();

        }
    }
}
