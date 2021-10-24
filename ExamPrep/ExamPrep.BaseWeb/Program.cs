using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPrep.BaseWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> structure = new Dictionary<string, List<string>>();

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "end")
            {
                string[] inputParams = inputLine.Split();

                string parent = inputParams[0];
                string child = inputParams[1];

                if (!structure.ContainsKey(parent)) structure[parent] = new List<string>();
                if (!structure.ContainsKey(child)) structure[child] = new List<string>();

                structure[parent].Add(child);
                structure[child].Add(parent);
            }

            // Take every node that has exactly 3 connections
            HashSet<string> connectedNodes = structure.Where(node => node.Value.Count == 3).Select(node => node.Key).ToHashSet();

            bool areAllConnected = true;

            foreach (var item in connectedNodes)
            {
                foreach (var otherItem in connectedNodes)
                {
                    if(otherItem == item)
                    {
                        continue;
                    }

                    if (!structure[item].Contains(otherItem))
                    {
                        areAllConnected = false;
                        break;
                    }
                }

                if(!areAllConnected) break;
            }

            if (connectedNodes.Count > 3 && !areAllConnected)
            {
                Dictionary<string, List<string>> webs = new Dictionary<string, List<string>>();

                connectedNodes = connectedNodes.Where(node =>
                {
                    List<string> connectedChildNodes = structure[node].Where(childNode => connectedNodes.Contains(childNode)).ToList();

                    if (connectedChildNodes.Count != 2)
                    {
                        return false;
                    }

                    string middleChildNode = structure[node]
                    .FirstOrDefault(childNode => !connectedNodes.Contains(childNode));

                    if(!webs.ContainsKey(middleChildNode))
                    {
                        webs[middleChildNode] = new List<string>();
                    }

                    webs[middleChildNode].Add(node);

                    HashSet<string> middleChildNodes = structure[middleChildNode].ToHashSet();

                    bool hasChildNodeMiddle =
                    middleChildNodes.Contains(node)
                    && middleChildNodes.Contains(connectedChildNodes[0])
                    && middleChildNodes.Contains(connectedChildNodes[1]);

                    return hasChildNodeMiddle;
                }).ToHashSet();

                KeyValuePair<string, List<string>> smallestWeb = 
                    webs.Where(web => web.Value.Count >= 3)
                    .OrderBy(web => web.Value.Count)
                    .FirstOrDefault();

                if (!smallestWeb.Equals(default))
                {
                    List<string> resultNodes = smallestWeb.Value;
                    resultNodes.Add(smallestWeb.Key);
                    
                    List<string> connections = new List<string>();

                    foreach (var parent in resultNodes)
                    {
                        foreach (var child in structure[parent])
                        {
                            connections.Add($"{parent} <-> {child}");
                        }
                    }

                    connections.OrderBy(x => x).ToList().ForEach(Console.WriteLine);
                }
                else
                {
                    Console.WriteLine("No web...");
                }
            }
            else if (connectedNodes.Count == 4 && areAllConnected)
            {
                List<string> connections = new List<string>();

                foreach (var parent in connectedNodes)
                {
                    foreach (var child in structure[parent])
                    {
                        connections.Add($"{parent} <-> {child}");
                    }
                }

                connections.OrderBy(x => x).ToList().ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("No web...");
            }
        }
    }
}
