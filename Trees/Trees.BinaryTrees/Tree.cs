using System;
using System.Collections.Generic;

namespace Trees.BinaryTrees
{
    public class Tree<T> where T : IComparable
    {
        public Tree()
        {
        }

        public Tree(T value)
        {
            this.Root = new Node<T>(value);
        }

        private void InternalPrint(Node<T> node, int indent = 0)
        {
            if (node == null) return;
            Console.WriteLine(new string(' ', indent) + node.Value);

            this.InternalPrint(node.LeftChild, indent + 2);
            this.InternalPrint(node.RightChild, indent + 2);
        }

        private void InternalRange(Node<T> node, List<T> result, int lowerBound, int upperBound)
        {
            if(node == null)
            {
                return;
            }

            if (node.Value.CompareTo(lowerBound) >= 0)
            {
                this.InternalRange(node.LeftChild, result, lowerBound, upperBound);
            }
            
            if (node.Value.CompareTo(lowerBound) >= 0 && node.Value.CompareTo(upperBound) <= 0)
            {
                result.Add(node.Value);
            }

            if (node.Value.CompareTo(upperBound) <= 0)
            {
                this.InternalRange(node.RightChild, result, lowerBound, upperBound);
            }
        }

        private void InternalPreOrder(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                action.Invoke(node.Value);
                this.InternalPreOrder(node.LeftChild, action);
                this.InternalPreOrder(node.RightChild, action);
            }
        }

        private void InternalPostOrder(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                this.InternalPostOrder(node.LeftChild, action);
                this.InternalPostOrder(node.RightChild, action);
                action.Invoke(node.Value);
            }
        }

        private void InternalInOrder(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                this.InternalInOrder(node.LeftChild, action);
                action.Invoke(node.Value);
                this.InternalInOrder(node.RightChild, action);
            }
        }

        private Node<T> Find(Node<T> current, T value)
        {
            if (current != null)
            {
                if (current.Value.Equals(value))
                {
                    return current;
                }

                if (current.Value.CompareTo(value) > 0)
                {
                    return this.Find(current.LeftChild, value);
                }

                if (current.Value.CompareTo(value) < 0)
                {
                    return this.Find(current.RightChild, value);
                }
            }

            return null;
        }

        public Node<T> Root { get; set; }

        public int Count { get; private set; }

        public void Add(T newNodeValue)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(newNodeValue);
                return;
            }

            Node<T> current = this.Root;

            while (true)
            {
                if (current.Value.CompareTo(newNodeValue) > 0)
                {
                    if (current.LeftChild == null)
                    {
                        current.LeftChild = new Node<T>(newNodeValue);
                        break;
                    }

                    current = current.LeftChild;
                }
                else if (current.Value.CompareTo(newNodeValue) < 0)
                {
                    if (current.RightChild == null)
                    {
                        current.RightChild = new Node<T>(newNodeValue);
                        break;
                    }

                    current = current.RightChild;
                }
                else
                {
                    break;
                }
            }

            this.Count++;
        }

        public Node<T> Search(T value)
        {
            return this.Find(this.Root, value);
        }

        public bool Contains(T value)
        {
            return this.Find(this.Root, value) != null;
        }

        public void Remove(T value)
        {
            if (this.Root.Value.Equals(value))
            {
                this.Root = null;
                return;
            }

            Node<T> current = this.Root;

            while (current != null)
            {
                if (current.LeftChild != null && current.LeftChild.Value.Equals(value))
                {
                    //Find and remove max < value and put min here

                    Node<T> currentMax = current.LeftChild.LeftChild;

                    while (currentMax.RightChild.RightChild != null)
                    {
                        currentMax = currentMax.RightChild;
                    }

                    Node<T> temp = currentMax.RightChild;
                    currentMax.RightChild = currentMax.RightChild.LeftChild;
                    temp.LeftChild = current.LeftChild.LeftChild;
                    temp.RightChild = current.LeftChild.RightChild;
                    current.LeftChild = temp;
                    break;
                }
                else if (current.RightChild != null && current.RightChild.Value.Equals(value))
                {
                    Node<T> currentMax = current.RightChild.LeftChild;

                    while (currentMax.RightChild.RightChild != null)
                    {
                        currentMax = currentMax.RightChild;
                    }

                    Node<T> temp = currentMax.RightChild;
                    currentMax.RightChild = currentMax.RightChild.LeftChild;
                    temp.RightChild = current.RightChild.RightChild;
                    temp.LeftChild = current.RightChild.LeftChild;
                    current.RightChild = temp;
                    break;
                }
                else
                {
                    if (current.Value.CompareTo(value) > 0)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            }
        }

        public void DeleteMin()
        {
            if(this.Root == null)
            {
                return;
            }

            if(this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }

            Node<T> current = this.Root;

            while(current.LeftChild.LeftChild != null)
            {
                current = current.LeftChild;
            }

            current.LeftChild = current.LeftChild.RightChild;
        }

        public IEnumerable<T> Range(int lowerBound, int upperBound)
        {
            List<T> result = new List<T>();

            this.InternalRange(this.Root, result, lowerBound, upperBound);

            return result;        
        }

        public void PreOrder(Action<T> action)
        {
            this.InternalPreOrder(this.Root, action);
        }

        public void PostOrder(Action<T> action)
        {
            this.InternalPostOrder(this.Root, action);
        }

        public void InOrder(Action<T> action)
        {
            this.InternalInOrder(this.Root, action);
        }

        public void Print(int indent = 0)
        {
            this.InternalPrint(this.Root, indent);
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

                if(current.LeftChild != null) traversalQueue.Enqueue(current.LeftChild);
                if(current.RightChild != null) traversalQueue.Enqueue(current.RightChild);
            }

            return result;
        }

    }
}
