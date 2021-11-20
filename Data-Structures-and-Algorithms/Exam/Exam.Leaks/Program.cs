using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Leaks
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "end")
            {
                string[] inputParams = inputLine.Split();

                if (!graph.ContainsKey(inputParams[0]))
                {
                    graph[inputParams[0]] = new List<string>();
                }

                if (!graph.ContainsKey(inputParams[1]))
                {
                    graph[inputParams[1]] = new List<string>();
                }

                graph[inputParams[0]].Add(inputParams[1]);
            }

            HashSet<string> traversedConnections = new HashSet<string>();

            int count = 0;

            graph
                .Where(node => graph.Any(otherNode => otherNode.Value.Contains(node.Key)))
                .Where(node => node.Value.Any(childNode => graph[childNode].Contains(node.Key)))
                .ToList().ForEach(node =>
                {
                    foreach (var child in node.Value)
                    {
                        string connection = $"{node.Key} <-> {child}";
                        if (!traversedConnections.Contains(connection))
                        {
                            traversedConnections.Add(connection);
                            traversedConnections.Add($"{child} <-> {node.Key}");
                            count++;
                            Console.WriteLine(connection);
                        }
                    }
                });

            if(count == 0)
            {
                Console.WriteLine("Disconnected");
            }
        }
    }
}
