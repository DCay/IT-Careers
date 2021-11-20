using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Exam.Permutations
{
    class Program
    {
        static void Permute(int index, string[] set, bool[] used, LinkedList<string> currentCombo, HashSet<string> combos)
        {
            if(index == set.Length)
            {
                combos.Add(string.Join("", currentCombo));
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if(!used[i])
                    {
                        used[i] = true;
                        currentCombo.AddLast(set[i]);
                        Permute(index + 1, set, used, currentCombo, combos);
                        currentCombo.RemoveLast();
                        used[i] = false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string[] set = Console.ReadLine().Split();
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HashSet<string> setOfChars = new HashSet<string>(set);

            HashSet<string> combos = new HashSet<string>();

            Permute(0, set, new bool[set.Length], new LinkedList<string>(), combos);

            bool hasAny = false;

            string inputLine = string.Empty;

            LinkedList<string> finalResult = new LinkedList<string>();

            while ((inputLine = Console.ReadLine()) != "end")
            {
                if(combos.Count == 0)
                {
                    break;
                }

                if(combos.Contains(inputLine))
                {
                    hasAny = true;
                    finalResult.AddLast(inputLine);
                    combos.Remove(inputLine);
                }
            }

            if (hasAny)
            {
                finalResult.OrderBy(x => x).ToList().ForEach(x => Console.WriteLine(string.Join(" ", x.ToCharArray())));
            }
            else
            {
                Console.WriteLine("No permutations...");
            }

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
