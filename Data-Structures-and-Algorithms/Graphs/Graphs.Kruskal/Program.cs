using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Kruskal
{
    struct Edge
    {
        public string Parent { get; set; }

        public string Child { get; set; }

        public int Weight { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
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

            Dictionary<string, List<string>> tree = GetMST(graph);
        }

        static string FindRoot(string node, Dictionary<string, List<string>> tree)
        {
            string result = null;

            if(tree[node].Count > 0)
            {
                result = node;
            }

            while(true)
            {
                bool foundParent = false;

                foreach (var item in tree)
                {
                    if(item.Value.Contains(node))
                    {
                        node = item.Key;
                        result = node;
                        foundParent = true;
                    }
                }

                if(!foundParent)
                {
                    break;
                }
            }

            return result;
        }

        static Dictionary<string, List<string>> GetMST(Dictionary<string, Dictionary<string, int>> graph)
        {
            List<Edge> edgesFromGraph = new List<Edge>();

            foreach (var node in graph)
            {
                foreach (var childNode in node.Value)
                {
                    if (!edgesFromGraph.Any(edge => edge.Parent == childNode.Key && edge.Child == node.Key))
                    {
                        edgesFromGraph.Add(new Edge
                        {
                            Parent = node.Key,
                            Child = childNode.Key,
                            Weight = childNode.Value
                        });
                    }
                }
            }

            Dictionary<string, List<string>> tree = new Dictionary<string, List<string>>();

            foreach (var node in graph)
            {
                tree.Add(node.Key, new List<string>());
            }

            LinkedList<Edge> edgesSet = new LinkedList<Edge>(
                edgesFromGraph
                .OrderBy(edge => edge.Weight)
                );
            
            while(edgesSet.Count > 0)
            {
                Edge current = edgesSet.First.Value;
                edgesSet.RemoveFirst();

                string rootParent = FindRoot(current.Parent, tree);
                string rootChild = FindRoot(current.Child, tree);

                if(rootParent == null && rootChild == null)
                {
                    tree[current.Parent].Add(current.Child);
                }
                else if(rootParent == null && rootChild != null)
                {
                    tree[rootChild].Add(current.Parent);
                }
                else if(rootParent != null && rootChild == null)
                {
                    tree[rootParent].Add(current.Child);
                }
                else if (rootParent != rootChild)
                {
                    tree[rootParent].Add(rootChild);
                }
            }

            return tree;
        }
    }
}
/*
A B C D E F G H I
A B 4
A C 5
B D 2
A D 9
C D 20
C E 7
E D 8
E F 12
H I 7
G H 8
G I 10
End
 */