using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    internal class Program
    {
        private class Valve
        {
            public string Name { get; set; }
            public int FlowRate { get; set; }
            public bool IsOpen { get; set; }
            public List<Valve> ConnectedValves { get; }

            public Valve(string name, int flowRate)
            {
                this.Name = name;
                this.FlowRate = flowRate;
                this.IsOpen = false;
                ConnectedValves = new List<Valve>();
            }

            public void AddValve(Valve valve)
            {
                ConnectedValves.Add(valve);
            }
        }

        static int GetMaxScore(Valve valve, int remainingTime)
        {
            // Remaining time is too short to do any other operations
            if (remainingTime < 2)
            {
                return 0;
            }

            // Remaining time only allows for opening of the current valve
            if (remainingTime == 2)
            {
                if (valve.IsOpen)
                {
                    return 0;
                }
                return valve.FlowRate;
            }




            int maxScoreWithValve = 0;
            int maxScoreWithoutValve = 0;

            // Calculate score by first opening the current valve. Skip this step if current valve's flow rate is 0
            if (!valve.IsOpen && valve.FlowRate != 0)
            {
                valve.IsOpen = true;
                foreach (var connectedValve in valve.ConnectedValves)
                {
                    maxScoreWithValve = Math.Max(maxScoreWithValve, GetMaxScore(connectedValve, remainingTime - 2));
                }
                maxScoreWithValve += valve.FlowRate * (remainingTime - 1);
                valve.IsOpen = false;
            }

            // Calculate score by not opening the current valve
            foreach (var connectedValve in valve.ConnectedValves)
            {
                maxScoreWithoutValve = Math.Max(maxScoreWithoutValve, GetMaxScore(connectedValve, remainingTime - 1));
            }

            return Math.Max(maxScoreWithValve, maxScoreWithoutValve);
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            // Process valves
            Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
            foreach (string line in lines)
            {
                string[] splits = line.Split(';');

                string[] valveSplits = splits[0].Split(' ');
                string valveName = valveSplits[1];
                int flowRate = int.Parse(valveSplits.Last().Split('=').Last());
                Valve valve = new Valve(valveName, flowRate);
                valves.Add(valveName, valve);
            }
            // Process valve connections
            foreach (string line in lines)
            {
                string valveName = line.Split(';')[0].Split(' ')[1];
                string[] connectedValves = line.Split(new string[] { "valves", "valve" }, StringSplitOptions.RemoveEmptyEntries).Last().Split(',').ToArray();
                foreach (string connectedValve in connectedValves)
                {
                    valves[valveName].AddValve(valves[connectedValve.Trim()]);
                }
            }

            const int time = 30;
            const string start = "AA";
            int maxScore = GetMaxScore(valves[start], time);
            Console.WriteLine("Max score is " + maxScore);


            Console.ReadLine();
        }
    }
}
