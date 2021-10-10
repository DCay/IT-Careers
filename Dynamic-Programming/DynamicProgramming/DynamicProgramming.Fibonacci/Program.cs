using System;
using System.Collections.Generic;

namespace DynamicProgramming.Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int previousPrevious = 0;
            int previous = 1;
            int current = previous + previousPrevious;

            for (int i = 3; i < n; i++)
            {
                previousPrevious = previous;
                previous = current;
                current = previousPrevious + previous;
            }

            Console.WriteLine(current);

            Dictionary<int, int> memorizationDictionary = new Dictionary<int, int>();
            memorizationDictionary.Add(0, 0);
            memorizationDictionary.Add(1, 1);

            Console.WriteLine(RecursiveFibunacci(n - 1, memorizationDictionary));
        }

        static int RecursiveFibunacci(int n, Dictionary<int, int> dictionary)
        {
            if(n == 1 || n == 0)
            {
                return dictionary[n];
            }

            if (!dictionary.ContainsKey(n))
            {
                dictionary[n] = RecursiveFibunacci(n - 1, dictionary) + RecursiveFibunacci(n - 2, dictionary);
            }
                
            return dictionary[n];
        }
    }
}
