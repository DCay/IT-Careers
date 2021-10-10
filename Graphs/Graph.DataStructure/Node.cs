using System.Collections.Generic;

namespace Graph.DataStructure
{
    public class Node
    {
        public Node(int value)
        {
            Value = value;
            ParentNodes = new List<Node>();
            ChildNodes = new List<Node>();
        }

        public int Value { get; set; }

        public List<Node> ChildNodes { get; set; }

        public List<Node> ParentNodes { get; set; }

        public void AddChild(int weight, Node child)
        {
            child.ParentNodes.Add(this);
            this.ChildNodes.Add(child);
        }

        public void AddChild(Node child)
        {
            this.AddChild(0, child);
        }
    }
}
