using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExamPrep.HalfPalindromes
{
    class Program
    {
        static void Permute(
            int index, 
            string[] set, 
            bool[] isUsed, 
            List<string> currentPermutation,
            List<string> reversedPermutation,
            HashSet<string> generated)
        {
            if(index >= set.Length)
            {
                string currentPermu = string.Join(" ", currentPermutation);
                string reversedPermu = string.Join(" ", reversedPermutation);
                if (!generated.Contains(currentPermu) && !generated
                    .Contains(reversedPermu)) //O(n)
                {
                    generated.Add(currentPermu);
                }
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if (!isUsed[i])
                    {
                        isUsed[i] = true;
                        currentPermutation.Add(set[i]);
                        reversedPermutation.Add(set[set.Length - i - 1]);
                        Permute(index + 1, set, isUsed, currentPermutation, reversedPermutation, generated);
                        reversedPermutation.RemoveAt(reversedPermutation.Count - 1);
                        currentPermutation.RemoveAt(currentPermutation.Count - 1);
                        isUsed[i] = false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            string[] letters = Enumerable.Range(97, 9).Select(x => (char) x + "").Reverse().ToArray();
            HashSet<string> generated = new HashSet<string>();
            string[] set = letters;
            //string[] set = Console.ReadLine().Split();
            stopwatch.Start();
            Permute(0, set, new bool[set.Length], new List<string>(), new List<string>(), generated);
            stopwatch.Stop();
            //generated.OrderBy(x => x).ToList().ForEach(Console.WriteLine);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
