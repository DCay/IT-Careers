using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Trees.BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
        {

            Tree<int> tree = new Tree<int>();

            Random random = new Random();

            HashSet<int> uniqueNums = new HashSet<int>();

            for (int i = 0; i < 100000; i++)
            {
                int num = random.Next(1, 1_500_000_000);

                while(uniqueNums.Contains(num))
                {
                    num = random.Next(1, 1_500_000_000);
                }

                uniqueNums.Add(num);
                tree.Add(num);
            }

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 10; i++)
            {
                stopwatch.Start();

                IEnumerable<int> result = tree.Range(1, 500_000_000);

                stopwatch.Stop();

                Console.WriteLine($"Benchmark No. {i} | Time: {stopwatch.ElapsedMilliseconds}ms");
                stopwatch.Reset();

            }



            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}


        }
    }
}
