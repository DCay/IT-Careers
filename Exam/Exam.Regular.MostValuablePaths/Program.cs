using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Regular.MostValuablePaths
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> tree = new Dictionary<int, List<int>>();

            int root = -1;

            string inputLine = string.Empty;

            while((inputLine = Console.ReadLine()) != "calculate")
            {
                int[] nums = inputLine.Split().Select(int.Parse).ToArray();

                if(root == -1)
                {
                    root = nums[0];
                }

                if (!tree.ContainsKey(nums[0])) tree[nums[0]] = new List<int>();
                tree[nums[0]].Add(nums[1]);
            }

            Dictionary<int, List<int>> sums = new Dictionary<int, List<int>>();

            Dfs(root, new List<int>(), tree, sums);

            foreach (var item in sums.OrderByDescending(x => x.Key))
            {
                Console.WriteLine($"{item.Key} -> {string.Join(", ", item.Value)}");
            }
        }

        static void Dfs(int currentNode, List<int> currentPath, Dictionary<int, List<int>> tree, Dictionary<int, List<int>> sums)
        {
            if(!tree.ContainsKey(currentNode))
            {
                currentPath.Add(currentNode);

                sums.Add(currentPath.Sum(), new List<int>(currentPath));
                
                currentPath.Remove(currentNode);
            }
            else
            {
                currentPath.Add(currentNode);

                foreach (var childNode in tree[currentNode])
                {
                    Dfs(childNode, currentPath, tree, sums);
                }

                currentPath.Remove(currentNode);
            }
        }
    }
}
