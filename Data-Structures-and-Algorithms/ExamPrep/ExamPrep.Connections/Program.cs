using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPrep.Connections
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            string inputLine = string.Empty;

            while((inputLine = Console.ReadLine()) != "end")
            {
                string[] inputParams = inputLine.Split();

                string parent = inputParams[0];
                string child = inputParams[1];

                if(!graph.ContainsKey(parent))
                {
                    graph[parent] = new List<string>();
                }

                if(!graph.ContainsKey(child))
                {
                    graph[child] = new List<string>();
                }

                graph[parent].Add(child);
            }

            //List<string> nodesWithParents = new List<string>();

            //foreach (var node in graph)
            //{
            //    string nodeName = node.Key;
            //    bool isChild = false;

            //    foreach (var otherNode in graph)
            //    {
            //        List<string> otherNodeChildren = otherNode.Value;
                    
            //        if(otherNodeChildren.Contains(nodeName))
            //        {
            //            isChild = true;
            //            break;
            //        }
            //    }

            //    if(isChild)
            //    {
            //        nodesWithParents.Add(nodeName);
            //    }
            //}

            //List<string> doubleConnectedNodes = new List<string>();

            //foreach (var innerNode in nodesWithParents)
            //{
            //    List<string> innerNodeChildren = graph[innerNode];

            //    foreach (var innerNodeChild in innerNodeChildren)
            //    {
            //        List<string> innerNodeChildChildren = graph[innerNodeChild];

            //        if(innerNodeChildChildren.Contains(innerNode))
            //        {
            //            doubleConnectedNodes.Add(innerNode);
            //            break;
            //        }
            //    }
            //}

            HashSet<string> traversedConnections = new HashSet<string>();

            int count = 0;

            graph
                .Where(node => graph.Any(otherNode => otherNode.Value.Contains(node.Key)))
                .Where(node => node.Value.Any(childNode => graph[childNode].Contains(node.Key)))
                .ToList()
                .ForEach(node =>
                {
                    foreach (var childNode in node.Value)
                    {
                        string connection = $"{node.Key} <-> {childNode}";
                        string reversedConnection = $"{childNode} <-> {node.Key}";

                        if(!traversedConnections.Contains(connection) && !traversedConnections.Contains(reversedConnection))
                        {
                            count++;
                            Console.WriteLine(connection);
                            traversedConnections.Add(connection);
                            traversedConnections.Add(reversedConnection);
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
