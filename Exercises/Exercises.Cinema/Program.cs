using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Cinema
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = Console.ReadLine().Split(", ").ToList();
            Dictionary<int, string> namesAndPlaces = new Dictionary<int, string>();

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "generate")
            {
                string[] inputParams = inputLine.Split(" - ");

                namesAndPlaces.Add(int.Parse(inputParams[1]), inputParams[0]);
            }

            foreach (var item in namesAndPlaces)
            {
                names.Remove(item.Value);
            }

            Permute(0, names, new bool[names.Count], new List<string>(), namesAndPlaces);
        }

        static void Permute(int index, List<string> set, bool[] used, List<string> currentPermutation, Dictionary<int, string> namesAndPlaces)
        {
            if (index >= set.Count)
            {
                List<string> finalPermutation = new List<string>();

                int totalSize = set.Count + namesAndPlaces.Count;

                for (int i = 0, permutationIndex = 0; i < totalSize; i++)
                {
                    if (namesAndPlaces.ContainsKey(i + 1))
                    {
                        finalPermutation.Add(namesAndPlaces[i + 1]);
                    }
                    else
                    {
                        finalPermutation.Add(currentPermutation[permutationIndex++]);
                    }
                }

                Console.WriteLine(string.Join(" ", finalPermutation));
            }
            else
            {
                for (int i = 0; i < set.Count; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        currentPermutation.Add(set[i]);
                        Permute(index + 1, set, used, currentPermutation, namesAndPlaces);
                        currentPermutation.RemoveAt(currentPermutation.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }
    }
}
