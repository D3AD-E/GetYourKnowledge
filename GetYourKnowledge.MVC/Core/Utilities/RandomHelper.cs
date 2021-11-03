using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Core.Utilities
{
    public static class RandomHelper
    {
        public static IEnumerable<int> GetRandomRange(int min, int max, int amount)
        {
            var rand = new Random();
            var randomList = new List<int>();
            while(randomList.Count < amount)
            {
                var num = rand.Next(min, max);
                if (!randomList.Contains(num))
                    randomList.Add(num);
            }
            return randomList;
        }

        public static int GetRandomNumberNotInCollection(int min, int max, IEnumerable<int> collection)
        {
            var rand = new Random();
            while (true)
            {
                var num = rand.Next(min, max);
                if (!collection.Contains(num))
                    return num;
            }
        }
    }
}
