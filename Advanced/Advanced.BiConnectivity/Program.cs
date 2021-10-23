using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.BiConnectivity
{
    class Program
    {
        static void Dfs(int currentNode, Dictionary<int, List<int>> graph, Dictionary<int, bool> isVisited, Action<int> action)
        {
            if (!isVisited[currentNode])
            {
                isVisited[currentNode] = true;

                // Process current node
                action.Invoke(currentNode);

                foreach (var childNode in graph[currentNode])
                {
                    Dfs(childNode, graph, isVisited, action);
                }
            }
        }

        static int GetConnectedComponentsCount(Dictionary<int, List<int>> graph)
        {
            int connectedComponents = 0;

            Dictionary<int, bool> isVisited = new Dictionary<int, bool>();

            foreach (var item in graph)
            {
                isVisited.Add(item.Key, false);
            }

            foreach (var node in graph)
            {
                if (!isVisited[node.Key])
                {
                    connectedComponents++;
                    Dfs(node.Key, graph, isVisited, (node) => { });
                }
            }

            return connectedComponents;
        }

        static Dictionary<int, List<int>> CopyGraph(Dictionary<int, List<int>> graph)
        {
            Dictionary<int, List<int>> copyGraph = new Dictionary<int, List<int>>();

            foreach (var node in graph)
            {
                copyGraph.Add(node.Key, new List<int>(node.Value));
            }

            return copyGraph;
        }

        static int GetBiConnectedComponentsCount(Dictionary<int, List<int>> graph)
        {
            int articulationPointsCount = 0;

            foreach (var node in graph)
            {
                Dictionary<int, List<int>> temporaryGraph = CopyGraph(graph);

                // Temprorary Removal of Node
                temporaryGraph.Remove(node.Key);

                foreach (var temporaryNode in temporaryGraph)
                {
                    temporaryNode.Value.Remove(node.Key);
                }

                int connectedComponentsCount = GetConnectedComponentsCount(temporaryGraph);

                if (connectedComponentsCount > 1)
                {
                    articulationPointsCount++;
                }
            }

            return articulationPointsCount; // Articulation Points
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, bool> isVisited = new Dictionary<int, bool>();

            int[] nodes = Console.ReadLine().Split().Select(int.Parse).ToArray();

            //Initialization
            foreach (var node in nodes)
            {
                graph.Add(node, new List<int>());
            }

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "End")
            {
                string[] inputParams = inputLine.Split();

                int parentNode = int.Parse(inputParams[0]);
                int childNode = int.Parse(inputParams[1]);

                graph[parentNode].Add(childNode);
                graph[childNode].Add(parentNode);
            }

            // Slow Algorithm
            Console.WriteLine(GetBiConnectedComponentsCount(graph) + 2);
        }
    }
}
