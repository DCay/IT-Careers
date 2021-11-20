using System;
using System.Collections.Generic;

namespace DynamicProgramming.RodCutting
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] prices = { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };//Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rodLength = int.Parse(Console.ReadLine());

            int[] bestPrices = new int[prices.Length];
            int[] bestCuts = new int[prices.Length];

            for (int i = 1; i <= rodLength; i++)
            {
                int currentMax = 0;

                for (int j = 1; j <= i; j++)
                {
                    if (currentMax < (prices[j] + bestPrices[i - j]))
                    {
                        currentMax = (prices[j] + bestPrices[i - j]);
                        bestCuts[i] = j;
                    }
                }

                bestPrices[i] = currentMax;
            }

            List<int> results = new List<int>();

            int currentLength = rodLength;

            while (currentLength > 0)
            {
                results.Add(bestCuts[currentLength]);
                currentLength -= bestCuts[currentLength];
            }

            Console.WriteLine(bestPrices[rodLength]);
            Console.WriteLine(string.Join(" ", results));
        }
    }
}
