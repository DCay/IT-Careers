using System;
using System.Collections.Generic;

namespace Trees.BasicTrees
{
    public class Tree<T>
    {
        public Tree(T value)
        {
            this.Root = new Node<T>(value);
        }

        private void InternalPrint(Node<T> node, int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + node.Value);

            foreach (var child in node.Children)
            {
                this.InternalPrint(child, indent + 2);
            }
        }

        private void InternalDFS(Node<T> node, Action<Node<T>> action)
        {
            foreach (var child in node.Children)
            {
                this.InternalDFS(child, action);
            }

            action.Invoke(node); // ACTION (VISIT)
        }

        private Node<T> Find(Node<T> current, T value)
        {
            if(current.Value.Equals(value))
            {
                return current;
            }

            foreach (var child in current.Children)
            {
                Node<T> result = this.Find(child, value);

                if(result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public Node<T> Root { get; set; }

        public void Add(T parentValue, T newNodeValue)
        {
            Node<T> parent = this.Find(this.Root, parentValue);

            if(parent != null)
            {
                parent.Children.Add(new Node<T>(newNodeValue));
            }
        }

        public void Print()
        {
            this.InternalPrint(this.Root);
        }

        public IEnumerable<T> OrderDFS()
        {
            List<T> result = new List<T>();

            InternalDFS(this.Root, (node) => result.Add(node.Value));

            return result;
        }

        public IEnumerable<T> OrderBFS()
        {
            List<T> result = new List<T>();
            Queue<Node<T>> traversalQueue = new Queue<Node<T>>();

            traversalQueue.Enqueue(this.Root);

            while(traversalQueue.Count > 0)
            {
                Node<T> current = traversalQueue.Dequeue();
                result.Add(current.Value); // ACTION (VISIT)

                foreach (var child in current.Children)
                {
                    traversalQueue.Enqueue(child);
                }
            }

            return result;
        }
    }
}
