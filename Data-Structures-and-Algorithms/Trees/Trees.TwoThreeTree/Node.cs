namespace Trees.TwoThreeTree
{
    public class Node<T>
    {
        public Node(T value)
        {
            this.LeftKey = value;
        }

        public Node(T left, T middle, T right)
        {
            this.LeftKey = middle;
            this.LeftChild = new Node<T>(left);
            this.RightChild = new Node<T>(right);
        }

        public bool IsTwoNode => this.RightKey.Equals(default(T));

        public bool IsThreeNode => !this.RightKey.Equals(default(T));

        public bool IsLeaf => this.LeftChild == null && this.MiddleChild == null && this.RightChild == null;

        public T LeftKey { get; set; }
        
        public T RightKey { get; set; }

        public Node<T> LeftChild { get; set; }

        public Node<T> MiddleChild { get; set; }

        public Node<T> RightChild { get; set; }
    }
}
