using System;
using System.Collections.Generic;

namespace Algorithms.Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());

            Dictionary<long, long> memorizedFibonacciNumbers = new Dictionary<long, long>();

            Console.WriteLine(Fibonacci(n, memorizedFibonacciNumbers));
        }

        private static long Fibonacci(long n, Dictionary<long, long> memorizedFibonacciNumbers)
        {
            if(n == 2 || n == 1)
            {
                return 1;
            }

            if(memorizedFibonacciNumbers.ContainsKey(n))
            {
                return memorizedFibonacciNumbers[n];
            }

            memorizedFibonacciNumbers.Add(n, Fibonacci(n - 1, memorizedFibonacciNumbers)
                + Fibonacci(n - 2, memorizedFibonacciNumbers));

            return memorizedFibonacciNumbers[n];
        }
    }
}
