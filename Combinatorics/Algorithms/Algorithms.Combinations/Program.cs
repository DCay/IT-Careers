using System;
using System.Linq;

namespace Algorithms.Combinations
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] set = Enumerable.Range(1, 5)
                .Select(x => ((char)(x + 64)).ToString())
                .ToArray();

            int positions = 2;

        }

        static void Combinations(
            string[] set, 
            int positions, 
            int index,
            int start,
            string[] currentCombination)
        {
            if(index >= positions)
            {
                //Console.WriteLine(string.Join(" ", currentCombination));
            }
            else
            {
                for (int i = start; i < set.Length; i++)
                {
                    currentCombination[index] = set[i];
                    Combinations(set, positions, index + 1, i + 1, currentCombination);
                }
            }
        }
    }
}
