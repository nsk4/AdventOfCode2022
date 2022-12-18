using System;
using System.Collections.Generic;

namespace Day11
{
    internal class Monkey
    {
        private int id;
        private List<long> items;
        private long? operationValue1;
        private long? operationValue2;
        private char operationSymbol;
        private int testValue;
        private int trueMonkey;
        private int falseMonkey;
        private int worryReductionFactor;

        private long multipleOptimization;

        private long totalInspects;

        public Monkey(int id, List<long> items, long? operationValue1, long? operationValue2, char operationSymbol, int testValue, int trueMonkey, int falseMonkey, int worryReductionFactor)
        {
            this.id = id;
            this.items = items;
            this.operationValue1 = operationValue1;
            this.operationValue2 = operationValue2;
            this.operationSymbol = operationSymbol;
            this.testValue = testValue;
            this.trueMonkey = trueMonkey;
            this.falseMonkey = falseMonkey;
            this.worryReductionFactor = worryReductionFactor;

            totalInspects = 0;
            multipleOptimization = 0;
        }

        public int GetItemCount()
        {
            return items.Count;
        }

        public int GetId()
        {
            return id;
        }

        public long GetTotalInspects()
        {
            return totalInspects;
        }

        public List<long> GetItems()
        {
            return items;
        }

        public void SetMultipleOptimization(long multipleOptimization)
        {
            this.multipleOptimization = multipleOptimization;
        }

        public (int, long) GetThrownItem()
        {
            if (items.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            long item = items[0];
            items.RemoveAt(0);

            totalInspects++;
            if (operationSymbol == '+')
            {
                item = operationValue1.GetValueOrDefault(item) + operationValue2.GetValueOrDefault(item);
            }
            else if (operationSymbol == '*')
            {
                item = operationValue1.GetValueOrDefault(item) * operationValue2.GetValueOrDefault(item);
            }
            else
            {
                throw new InvalidOperationException();
            }

            if (worryReductionFactor != 1)
            {
                item /= worryReductionFactor;
            }

            // Reduce for the minimum denominator
            if (multipleOptimization != 0 && item > multipleOptimization)
            {
                item -= multipleOptimization * (item / multipleOptimization);
            }

            if (item % testValue == 0)
            {
                return (trueMonkey, item);
            }
            else
            {
                return (falseMonkey, item);
            }
        }

        public void AddWorryItem(long item)
        {
            items.Add(item);
        }


    }
}
