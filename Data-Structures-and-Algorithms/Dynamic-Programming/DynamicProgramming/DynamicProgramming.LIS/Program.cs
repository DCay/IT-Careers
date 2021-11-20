using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming.LIS
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3 14 5 12 15 7 8 9 11 10 1
            int[] sequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int[] lengths = new int[sequence.Length];
            int[] previousIndexes = new int[sequence.Length];

            int maxLengthIndex = -1;
            int maxLength = 0;

            for (int i = 0; i < sequence.Length; i++)
            {
                lengths[i] = 1;
                previousIndexes[i] = -1;

                for (int j = 0; j < i; j++)
                {
                    if (sequence[i] > sequence[j] && lengths[j] + 1 > lengths[i])
                    {
                        lengths[i] = lengths[j] + 1;
                        previousIndexes[i] = j;
                    }
                }

                if (lengths[i] > maxLength)
                {
                    maxLength = lengths[i];
                    maxLengthIndex = i;
                }
            }

            Stack<int> result = new Stack<int>();

            while (maxLengthIndex >= 0)
            {
                result.Push(sequence[maxLengthIndex]);
                maxLengthIndex = previousIndexes[maxLengthIndex];
            }

            //Console.WriteLine(string.Join(" ", previousIndexes));
            //Console.WriteLine(string.Join(" ", lengths));
            //Console.WriteLine(maxLengthIndex);


            Console.WriteLine(string.Join(" ", result));
        }
    }
}
