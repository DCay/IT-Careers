using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPrep.MostValuablePaths
{
    class Program
    {

        // a
        // b
        // c

        // strign(a + b + c) int.Parse
        // string(a + c + b) int.Parse
        // ...

        static void Dfs(
            int currentNode, 
            Dictionary<int, List<int>> tree, 
            Dictionary<long, List<int>> sums,
            List<int> currentPath)
        {
            if (tree[currentNode].Count == 0)
            {
                currentPath.Add(currentNode);

                long sum = 0;

                for (int i = 0; i < currentPath.Count; i++) sum += currentPath[i];

                sums.Add(sum, new List<int>(currentPath));

                currentPath.RemoveAt(currentPath.Count - 1);
            }
            else
            {
                currentPath.Add(currentNode);
                
                foreach (var childNode in tree[currentNode])
                {
                    Dfs(childNode, tree, sums, currentPath);
                }

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<int>> tree = new Dictionary<int, List<int>>();
            string inputLine = string.Empty;

            bool hasSetRoot = false;
            int root = 0;

            while((inputLine = Console.ReadLine()) != "calculate")
            {
                string[] inputParams = inputLine.Split();

                int parent = int.Parse(inputParams[0]);
                int child = int.Parse(inputParams[1]);

                if (!hasSetRoot)
                {
                    hasSetRoot = true;
                    root = parent;
                }

                if(!tree.ContainsKey(parent))
                {
                    tree[parent] = new List<int>();
                }

                tree[parent].Add(child);
                tree[child] = new List<int>();
            }

            Dictionary<long, List<int>> sums = new Dictionary<long, List<int>>();

            Dfs(root, tree, sums, new List<int>());

            foreach (var sum in sums.OrderByDescending(x => x.Key).Take(3))
            {
                Console.WriteLine($"{sum.Key} -> {string.Join(", ", sum.Value)}");
            }
        }
    }
}
