using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExamPrep.Permutations
{
    class Program
    {
        static void Permute(int index, string[] set, bool[] used, List<string> currentPermutation, HashSet<string> totalPermutations)
        {
            if(index >= set.Length)
            {
                totalPermutations.Add(string.Join(" ", currentPermutation));
            } 
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if(!used[i])
                    {
                        used[i] = true;
                        currentPermutation.Add(set[i]);
                        Permute(index + 1, set, used, currentPermutation, totalPermutations);
                        currentPermutation.RemoveAt(currentPermutation.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] set = Console.ReadLine().Split();

            HashSet<string> permutations = new HashSet<string>();

            Permute(0, set, new bool[set.Length], new List<string>(), permutations);

            string inputLine = string.Empty;

            List<string> result = new List<string>();

            // 120
            // 1 000 000

            while((inputLine = Console.ReadLine()) != "end")
            {
                string currentLineAsPermutation = string.Join(" ", inputLine.ToCharArray()); 

                if(permutations.Contains(currentLineAsPermutation))
                {
                    result.Add(currentLineAsPermutation);
                    permutations.Remove(currentLineAsPermutation);
                }

                if (permutations.Count == 0)
                {
                    break;
                }
            }

            if(result.Count == 0)
            {
                Console.WriteLine("No permutations...");
            }
            else
            {
                foreach (var item in result.OrderBy(x => x))
                {
                    Console.WriteLine(item);
                }
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
