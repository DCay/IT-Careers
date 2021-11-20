using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.DataStructure
{
    public class Graph<T>
    {
        private List<Node> nodes;

        private List<T> values;

        public Graph()
        {
            this.nodes = new List<Node>();
            this.values = new List<T>();
        }

        public List<Node> Nodes => this.nodes;

        public List<T> Values => this.values;

        private void Dfs(Node currentNode, bool[] visitedNodes, Action<T> action)
        {
            if (currentNode == null)
            {
                return;
            }

            int currentNodeHashCode = currentNode.GetHashCode();

            if (!visitedNodes[currentNode.Value])
            {
                visitedNodes[currentNode.Value] = true;
                action.Invoke(this.values[currentNode.Value]);

                foreach (var child in currentNode.ChildNodes)
                {
                    this.Dfs(child, visitedNodes, action);
                }
            }
        }

        public void Add(T value)
        {
            this.values.Add(value);
            this.nodes.Add(new Node(this.values.Count - 1));
        }

        public void Connect(T parent, T child, int weight = 0)
        {
            Node parentNode = this.nodes[this.values.IndexOf(parent)];
            Node childNode = this.nodes[this.values.IndexOf(child)];

            parentNode.AddChild(weight, childNode);
        }

        public void ConnectBoth(T parent, T child, int weight = 0)
        {
            this.Connect(parent, child, weight);
            this.Connect(child, parent, weight);
        }

        public void Dfs(Action<T> action)
        {
            bool[] visitedNodes = new bool[this.values.Count];

            foreach (var node in this.nodes)
            {
                this.Dfs(node, visitedNodes, action);
            }
        }

        public void Bfs(Action<T> action)
        {
            bool[] visitedNodes = new bool[this.values.Count];

            foreach (var node in this.nodes)
            {
                if (!visitedNodes[node.Value])
                {
                    Queue<Node> nodes = new Queue<Node>();

                    nodes.Enqueue(node);

                    while (nodes.Count > 0)
                    {
                        Node currentNode = nodes.Dequeue();

                        if (!visitedNodes[currentNode.Value])
                        {
                            visitedNodes[currentNode.Value] = true;

                            action.Invoke(this.values[currentNode.Value]);

                            foreach (var childNode in currentNode.ChildNodes)
                            {
                                nodes.Enqueue(childNode);
                            }
                        }
                    }
                }
            }
        }

        public int ConnectedComponents()
        {
            int connectedComponentsCount = 0;
            bool[] visitedNodes = new bool[this.values.Count];

            foreach (var node in this.nodes)
            {
                if (!visitedNodes[node.Value])
                {
                    connectedComponentsCount++;
                    Queue<Node> nodes = new Queue<Node>();

                    nodes.Enqueue(node);

                    while (nodes.Count > 0)
                    {
                        Node currentNode = nodes.Dequeue();

                        if (!visitedNodes[currentNode.Value])
                        {
                            visitedNodes[currentNode.Value] = true;

                            foreach (var childNode in currentNode.ChildNodes)
                            {
                                nodes.Enqueue(childNode);
                            }
                        }
                    }
                }
            }

            return connectedComponentsCount;
        }

        public Graph<T> GetCopy()
        {
            Graph<T> copyGraph = new Graph<T>();

            foreach (var value in this.values)
            {
                copyGraph.Add(value);
            }

            foreach (var node in this.nodes)
            {
                foreach (var childNode in node.ChildNodes)
                {
                    copyGraph.Connect(this.values[node.Value], this.values[childNode.Value]);
                }
            }

            return copyGraph;
        }

        public List<T> TopologicalSort()
        {
            List<T> result = new List<T>();

            Graph<T> copyGraph = this.GetCopy();
            List<Node> copyGraphList = copyGraph.Nodes;

            while (copyGraphList.Count > 0)
            {
                Node onlyParentNode = copyGraphList.FirstOrDefault(node => node.ParentNodes.Count == 0);
                copyGraphList.Remove(onlyParentNode);

                foreach (var childNode in onlyParentNode.ChildNodes)
                {
                    childNode.ParentNodes.Remove(onlyParentNode);
                }

                result.Add(this.values[onlyParentNode.Value]);
            }

            return result;
        }
    }
}
