using System;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int packetWindowLength = 4;
            const int messageWindowLength = 14;
            string input = System.IO.File.ReadAllLines("input.txt")[0];

            // Slide window to get the start of the packet
            for (int i = 0; i < input.Length - packetWindowLength; i++)
            {
                string marker = input.Substring(i, packetWindowLength);
                int distinct = marker.Distinct().ToArray().Length;
                if (distinct == packetWindowLength)
                {
                    Console.WriteLine("Packet - Marker: " + marker + ", Position: " + i + ", Start: " + (i + packetWindowLength));
                    break;
                }
            }

            // Slide window to get the start of the message
            for (int i = 0; i < input.Length - messageWindowLength; i++)
            {
                string marker = input.Substring(i, messageWindowLength);
                int distinct = marker.Distinct().ToArray().Length;
                if (distinct == messageWindowLength)
                {
                    Console.WriteLine("Message - Marker: " + marker + ", Position: " + i + ", Start: " + (i + messageWindowLength));
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
