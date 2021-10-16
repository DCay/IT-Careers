using System;
using System.Collections.Generic;

namespace Graphs.ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] destinations = Console.ReadLine().Split();
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            //Initialization
            foreach (var destinationElem in destinations)
            {
                graph.Add(destinationElem, new List<string>());
            }

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "End")
            {
                string[] inputParams = inputLine.Split();

                string parentDestination = inputParams[0];
                string childDestination = inputParams[1];

                graph[parentDestination].Add(childDestination);
                graph[childDestination].Add(parentDestination);
            }

            string[] shortestPathNodes = Console.ReadLine().Split();

            string origin = shortestPathNodes[0];
            string destination = shortestPathNodes[1];

            // origin - destination - paths of nodes
            Dictionary<string, List<string>> paths = new Dictionary<string, List<string>>();

            HashSet<string> usedNodes = new HashSet<string>();
            Queue<string> nodesQueue = new Queue<string>();
            nodesQueue.Enqueue(origin);

            string previous = null;

            while (nodesQueue.Count > 0)
            {
                string currentNode = nodesQueue.Dequeue(); // Current

                if (!usedNodes.Contains(currentNode))
                {
                    usedNodes.Add(currentNode);
                    
                    if (previous != null)
                    {
                        foreach (var childNode in graph[currentNode])
                        {
                            if(!paths.ContainsKey(childNode))
                            {
                                paths[childNode] = new List<string>(paths[currentNode]);
                            }

                            if (!paths[childNode].Contains(childNode))
                            {
                                paths[childNode].Add(childNode);
                            }

                            nodesQueue.Enqueue(childNode);
                        }
                    }
                    else
                    {
                        previous = currentNode;

                        foreach (var childNode in graph[currentNode])
                        {
                            paths[childNode] = new List<string>();

                            paths[childNode].Add(previous);
                            paths[childNode].Add(childNode);

                            nodesQueue.Enqueue(childNode);
                        }
                    }
                }
            }

            Console.WriteLine(string.Join(" -> ", paths[destination]) + "; Length = " + (paths[destination].Count - 1));
        }
    }
}
