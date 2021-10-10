using System;
using System.Collections.Generic;

namespace Exam.Regular.HalfPalindromes
{
    class Program
    {
        static void Permute(int index, string[] set, bool[] used, List<string> current, LinkedList<string> reversed, HashSet<string> usedCombos)
        {
            if(index == set.Length)
            {
                string currentCombo = string.Join(" ", current);
                string currentReversedCombo = string.Join(" ", reversed);
                
                if (!usedCombos.Contains(currentCombo) && !usedCombos.Contains(currentReversedCombo))
                {
                    Console.WriteLine(currentCombo);
                    usedCombos.Add(currentCombo);
                    usedCombos.Add(currentReversedCombo);
                }

                return;
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if(!used[i])
                    {
                        used[i] = true;
                        current.Add(set[i]);
                        reversed.AddFirst(set[i]);
                        Permute(index + 1, set, used, current, reversed, usedCombos);
                        reversed.RemoveFirst();
                        current.RemoveAt(current.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // You are forming palindromes from a set of letters.
            // reversed don't count

            string[] set = Console.ReadLine().Split();
                        Permute(0, set, new bool[set.Length], new List<string>(), new LinkedList<string>(), new HashSet<string>());
        }
    }
}
