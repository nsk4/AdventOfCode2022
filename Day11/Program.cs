using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    internal class Program
    {
        static void CalculateMonkeyBusinessScore(String[] lines, int roundLimit, int worryReduction)
        {
            List<Monkey> monkeys = new List<Monkey>();
            long multipleOptimization = 1;
            for (int i = 0; i < lines.Length; i += 7)
            {
                int monkeyId = int.Parse(lines[i].Trim(':').Split(' ')[1]);
                List<long> items = lines[i + 1].Split(':')[1].Split(',').Select(x => long.Parse(x)).ToList();

                string[] operation = lines[i + 2].Split('=')[1].Trim().Split(' ');
                long? operationValue1 = null;
                long? operationValue2 = null;
                char operationSymbol = operation[1][0];
                if (!operation[0].Equals("old"))
                {
                    operationValue1 = long.Parse(operation[0]);
                }
                if (!operation[2].Equals("old"))
                {
                    operationValue2 = long.Parse(operation[2]);
                }

                int testValue = int.Parse(lines[i + 3].Split(' ').Last());
                int trueMonkey = int.Parse(lines[i + 4].Split(' ').Last());
                int falseMonkey = int.Parse(lines[i + 5].Split(' ').Last());

                multipleOptimization *= testValue;

                monkeys.Add(new Monkey(monkeyId, items, operationValue1, operationValue2, operationSymbol, testValue, trueMonkey, falseMonkey, worryReduction));
            }

            // Set common multiple optimization
            monkeys.ForEach(x => x.SetMultipleOptimization(multipleOptimization));

            for (int i = 1; i <= roundLimit; i++)
            {
                for (int j = 0; j < monkeys.Count; j++)
                {
                    Monkey monkey = monkeys.Find(x => x.GetId() == j);
                    while (monkey.GetItemCount() > 0)
                    {
                        var thrownItem = monkey.GetThrownItem();
                        monkeys.Find(x => x.GetId() == thrownItem.Item1).AddWorryItem(thrownItem.Item2);
                    }
                }
            }

            Console.WriteLine("== Finish ==");
            monkeys.ForEach(monkey => { Console.WriteLine("Monkey: " + monkey.GetId() + " has " + monkey.GetItemCount() + " with total of " + monkey.GetTotalInspects() + " inspects."); });
            monkeys.Sort((x, y) => y.GetTotalInspects().CompareTo(x.GetTotalInspects()));
            long monkeyBusinessScore = monkeys[0].GetTotalInspects() * monkeys[1].GetTotalInspects();
            Console.WriteLine("Monkey business score is " + monkeyBusinessScore);
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            // Part 1
            CalculateMonkeyBusinessScore(lines, 20, 3);

            // Part 2
            CalculateMonkeyBusinessScore(lines, 10000, 1);

            Console.ReadLine();
        }
    }
}
