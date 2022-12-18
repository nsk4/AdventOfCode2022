using System;
using System.Collections.Generic;

namespace Day13
{
    internal class Program
    {
        private class Item
        {
            public int? Value { get; set; }
            private List<Item> items;

            public Item()
            {
                items = new List<Item>();
            }

            public Item Parse(string text)
            {
                for (int c = 0; c < text.Length; c++)
                {
                    if (text[c].Equals('['))
                    {
                        int numBrackets = 1;
                        for (int i = c + 1; i < text.Length; i++)
                        {
                            if (text[i].Equals('['))
                            {
                                // New list starts
                                numBrackets++;

                            }
                            else if (text[i].Equals(']'))
                            {
                                // List ends
                                numBrackets--;
                                if (numBrackets == 0)
                                {
                                    // Initial list ended
                                    Item item = new Item().Parse(text.Substring(c + 1, i - c));
                                    items.Add(item);
                                    c = i;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Extract next element
                        for (int i = c + 1; i < text.Length; i++)
                        {
                            if (text[i].Equals(',') || text[i].Equals(']'))
                            {
                                Item item = new Item();
                                item.Value = int.Parse(text.Substring(c, i - c));
                                items.Add(item);
                                c = i + 1;
                                break;
                            }
                        }
                    }
                }

                return this;
            }

        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            int indiciesSum = 0;

            Item item = new Item().Parse(lines[0]);
            Console.WriteLine();

            /*
            for (int i = 0; i < lines.Length; i += 3)
            {
                string left = lines[i];
                string right = lines[i + 1];



            }
            */

            Console.ReadLine();
        }
    }
}
