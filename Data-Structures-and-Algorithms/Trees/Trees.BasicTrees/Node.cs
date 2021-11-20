
using System.Collections.Generic;
using System.Linq;

namespace Trees.BasicTrees
{
    public class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
            this.Children = new List<Node<T>>();
        }


        public Node(T value, params Node<T>[] children) : this(value)
        {
            children.ToList().ForEach(child => this.Children.Add(child));
        }

        public T Value { get; set; }

        public List<Node<T>> Children { get; set; }
    }
}
