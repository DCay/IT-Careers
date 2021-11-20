using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms.Variations
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] set = Enumerable.Range(1, 4)
                .Select(x => ((char)(x + 64)).ToString())
                .ToArray();
            bool[] used = new bool[set.Length];
            int positions = 1;

            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            Variations(set, used, 0, positions, new List<int>());
            //Console.WriteLine(count);
            //watch.Stop();
            //Console.WriteLine($"RECURSIVE: {watch.ElapsedMilliseconds} ms");
            //watch.Reset();
            //watch.Start();
            //IterativeVariations(set);
            //watch.Stop();
            //Console.WriteLine($"ITERATIVE: {watch.ElapsedMilliseconds} ms");
        }

        static int count = 0;

        private static void Variations(
            string[] set, 
            bool[] used, 
            int index, 
            int positions, 
            List<int> currentVariation)
        {
            if(index >= positions)
            {
                count++;
                Console.WriteLine(string.Join(" ", currentVariation));
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    //if (!used[i])
                    //{
                    //    used[i] = true;
                        currentVariation.Add(i);
                        Variations(set, used, index + 1, positions, currentVariation);
                        currentVariation.RemoveAt(currentVariation.Count - 1);
                    //    used[i] = false;
                    //}
                }
            }
        }

        private static void IterativeVariations(string[] set)
        {
            int n = 4;
            int k = 4;
            int[] array = new int[k];

            while(true)
            {
                Console.WriteLine(string.Join(" ", array));
                int index = k - 1;
                
                while(index >= 0 && array[index] == n - 1)
                {
                    index--;
                }

                if(index < 0)
                {
                    break;
                }

                array[index]++;

                for (int i = index + 1; i < k; i++)
                {
                    array[i] = 0;
                }
            }
        }
    }
}