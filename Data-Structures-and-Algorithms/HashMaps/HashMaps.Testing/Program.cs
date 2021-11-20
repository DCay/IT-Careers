using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HashMaps.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            // 7 19
            // 7 23
            // 7 14
            // 19 4
            // 19 10
            // 14 41
            // 14 9

            Dictionary<int, List<int>> tree = new Dictionary<int, List<int>>();

            int root = -1;

            string inputLine = string.Empty;

            while((inputLine = Console.ReadLine()) != "exit")
            {
                int[] inputParams = inputLine.Split().Select(int.Parse).ToArray();

                int parent = inputParams[0];
                int child = inputParams[1];

                if(tree.Count == 0)
                {
                    root = parent;
                }

                if(!tree.ContainsKey(parent))
                {
                    tree.Add(parent, new List<int>());
                }

                tree[parent].Add(child);
            }

            Dfs(root, tree);
        }

        static void Dfs(int currentNode, Dictionary<int, List<int>> tree)
        {
            Console.WriteLine(currentNode);

            if(tree.ContainsKey(currentNode))
            {
                foreach (var child in tree[currentNode])
                {
                    Dfs(child, tree);
                }
            }
        }
    }
}
