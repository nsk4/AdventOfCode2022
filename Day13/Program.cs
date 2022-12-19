using System;
using System.Collections.Generic;

namespace Day13
{
    internal class Program
    {
        private class Item
        {
            public int? Value { get; set; }
            public bool IsDividerPacket { get; set; }
            private List<Item> items;

            public Item()
            {
                Value = null;
                IsDividerPacket = false;
                items = new List<Item>();
            }

            public Item Parse(string text, bool divider = false)
            {
                // If packet for parsing is divider set its flag. 
                IsDividerPacket = divider;

                // Iterate through characters in a string.
                for (int c = 0; c < text.Length; c++)
                {
                    // Next element is the start of a new list. Find the end of it and recursively process it.
                    if (text[c].Equals('['))
                    {
                        // Continue untill all opened brackets are closed.
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
                                    c = i+1; // +1 because ']' is followed by ','
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Next element is a digit.
                        for (int i = c; i < text.Length; i++)
                        {
                            if(c == i)
                            {
                                continue;
                            }
                            else if (text[i].Equals(',') || text[i].Equals(']'))
                            {
                                Item item = new Item();
                                item.Value = int.Parse(text.Substring(c, i - c));
                                items.Add(item);
                                c = i;
                                break;
                            }
                        }
                    }
                }

                return this;
            }

            public int GetListLength()
            {
                return items.Count;
            }

            public List<Item> GetListItems ()
            {
                return items;
            }

            public int CompareTo(Item item)
            {
                Item left = this;
                Item right = item;

                if(left.Value.HasValue && right.Value.HasValue)
                {
                    // If both items only have value then compare them.
                    return left.Value.Value.CompareTo(right.Value.Value);
                }
                else if(left.Value.HasValue)
                {
                    // If left item has value then wrap it as list.
                    left = new Item();
                    left.items.Add(this);
                }
                else if(right.Value.HasValue)
                {
                    // If right item has value then wrap it as list.
                    right = new Item();
                    right.items.Add(item);
                }

                for (int i=0; i<Math.Max(left.GetListLength(), right.GetListLength()); i++)
                {
                    // Compare two lists.
                    if(i == left.GetListLength())
                    {
                        // If left list is first to run out then they are in order.
                        return -1;
                    }
                    else if(i == right.GetListLength())
                    {
                        // If right list is first to run out then they are not in order.
                        return 1;
                    }
                    else
                    {
                        // Compare 2 lists, if they are not in order return, otherwise continue for other elements.
                        int comp = left.GetListItems()[i].CompareTo(right.GetListItems()[i]);
                        if(comp == 0)
                        {
                            continue;
                        }
                        return comp;
                    }
                }

                return 0;
            }

            public void SortPackets()
            {
                if(items.Count > 0)
                {
                    items.Sort((x, y) => x.CompareTo(y));
                }
            }
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            #region Part 1 - Compare packets
            int indiciesSum = 0;
            int index = 1;
            for (int i = 0; i < lines.Length; i += 3)
            {
                string leftString = lines[i];
                string rightString = lines[i + 1];
                Item left = new Item().Parse(leftString);
                Item right = new Item().Parse(rightString);

                int comparison = left.CompareTo(right);
                Console.WriteLine("Index: " + index + ", order: " + comparison);
                if(comparison != 1) 
                {
                    indiciesSum += index;
                }
                index++;
            }
            Console.WriteLine("Indicies sum: " + indiciesSum);
            #endregion

            #region Part 2 - Order packets
            Item packetList = new Item();
            foreach(string line in lines)
            {
                if(lines.Length == 0)
                {
                    continue;
                }

                packetList.Parse(line);
            }
            packetList.GetListItems().Add(new Item().Parse("[[2]]", true));
            packetList.GetListItems().Add(new Item().Parse("[[6]]", true));
            packetList.SortPackets();
            int decoderKey = 1;
            for(int i=0; i<packetList.GetListLength(); i++)
            {
                if (packetList.GetListItems()[i].IsDividerPacket)
                {
                    decoderKey *= i+1;
                }
            }
            Console.WriteLine("Decoder key: " + decoderKey);
            #endregion

            Console.ReadLine();
        }
    }
}
