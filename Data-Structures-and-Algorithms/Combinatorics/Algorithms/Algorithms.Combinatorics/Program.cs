using System;
using System.Collections.Generic;

namespace Algorithms.Combinatorics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] set = { "A", "B", "C" };
            bool[] used = new bool[set.Length];
            int positions = 3;

            Permutations(set, used, 0, positions, new List<string>());
        }

        static void Permutations(
            string[] set,
            bool[] used,
            int index,
            int positions,
            List<string> currentPermutation)
        {
            if (index >= positions)
            {
                Console.WriteLine(string.Join(" ", currentPermutation));
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        currentPermutation.Add(set[i]);
                        Permutations(set, used, index + 1, positions, currentPermutation);
                        currentPermutation.RemoveAt(currentPermutation.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }
    }
}
