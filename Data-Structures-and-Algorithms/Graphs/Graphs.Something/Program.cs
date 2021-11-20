using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs.Something
{
    class Program
    {
        static void Main(string[] args)
        {
            // Warmup
            // Directed, Weighted, Cyclic?, Connected?

            // Destinations
            // Name, Distance to child

            // Destionation1 Destionation2 Destionation3 ...
            // DestionationX DestionationY (Weight)
            // ...
            // End
            // BFS
            // After, find shortest path from DestionationA to DestionationB

            string[] destinations = Console.ReadLine().Split();
            Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>();

            //Initialization
            foreach (var destinationElem in destinations)
            {
                graph.Add(destinationElem, new Dictionary<string, int>());
            }

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "End")
            {
                string[] inputParams = inputLine.Split();

                string parentDestination = inputParams[0];
                string childDestination = inputParams[1];
                int weight = int.Parse(inputParams[2]);

                graph[parentDestination].Add(childDestination, weight);
                graph[childDestination].Add(parentDestination, weight);
            }

            string[] inputDestinations = Console.ReadLine().Split();
            string origin = inputDestinations[0];
            string destination = inputDestinations[1];

            // From this point on, no hope remains
            // Dijkstra
            Dictionary<string, int> distances = new Dictionary<string, int>();
            Dictionary<string, bool> isVisited = new Dictionary<string, bool>();
            Dictionary<string, string> previous = new Dictionary<string, string>();

            for (int i = 0; i < destinations.Length; i++)
            {
                distances.Add(destinations[i], int.MaxValue);
                isVisited.Add(destinations[i], false);
            }

            distances[origin] = 0;

            // BFS
            LinkedList<string> nodesQueue = new LinkedList<string>();
            nodesQueue.AddLast(origin);

            while (nodesQueue.Count > 0)
            {
                // Dequeue
                string current = nodesQueue.First.Value;
                nodesQueue.RemoveFirst();

                if (!isVisited[current])
                {
                    isVisited[current] = true;

                    foreach (var child in graph[current])
                    {
                        if (!isVisited[child.Key])
                        {
                            // DP
                            int currentDistance = distances[current] + child.Value;

                            if(currentDistance < distances[child.Key])
                            {
                                distances[child.Key] = currentDistance;
                                previous[child.Key] = current;
                            }

                            nodesQueue.AddLast(child.Key);
                        }
                    }

                    // Priority Queue
                    nodesQueue = new LinkedList<string>(nodesQueue.OrderBy(node => distances[node]).ToList());
                }
            }

            // Reconstruct Solution

            Stack<string> path = new Stack<string>();

            string currentNode = destination;

            path.Push(currentNode);

            while(previous.ContainsKey(currentNode))
            {
                currentNode = previous[currentNode];
                path.Push(currentNode);
            }

            Console.WriteLine(string.Join(" -> ", path) + $" ({distances[destination]})");
        }



        /*
Sofia Plovdiv Burgas Varna Ruse StaraZagora Silistra Blagoevgrad Lom Vidin Vraca
Sofia Plovdiv 150
Plovdiv StaraZagora 150
StaraZagora Burgas 250
Burgas Varna 100
Ruse Varna 300
Ruse Silistra 100
Varna Silistra 250
Sofia Blagoevgrad 125
Sofia Vraca 125 // 75
Vraca Lom 75
Vraca Vidin 125
Vidin Lom 150
Vraca Ruse 275 // 250
Lom Ruse 300
End
         */
    }
}
