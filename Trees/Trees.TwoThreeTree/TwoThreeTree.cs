using System;

namespace Trees.TwoThreeTree
{
    public class TwoThreeTree<T> where T : IComparable
    {
        public TwoThreeTree()
        {
        }

        private Node<T> InternalInsert(Node<T> current, T value)
        {
            if (current.IsLeaf)
            {
                if (current.IsTwoNode)
                {
                    if (current.LeftKey.CompareTo(value) > 0)
                    {
                        current.RightKey = current.LeftKey;
                        current.LeftKey = value;
                    }
                    else
                    {
                        current.RightKey = value;
                    }
                }
                else
                {
                    T left = current.LeftKey;
                    T middle = value;
                    T right = current.RightKey;

                    if (middle.CompareTo(left) < 0)
                    {
                        return new Node<T>(middle, left, right); // Middle is lowest value
                    }
                    else if (middle.CompareTo(left) > 0 && middle.CompareTo(right) < 0)
                    {
                        return new Node<T>(left, middle, right); // Middle is middle value
                    }
                    else if (middle.CompareTo(right) > 0)
                    {
                        return new Node<T>(left, right, middle); // Middle is highest value
                    }
                }
            }
            else 
            {
                Node<T> restructuredNode = null;

                if(current.IsTwoNode && current.LeftKey.CompareTo(value) > 0)
                {
                    restructuredNode = this.InternalInsert(current.LeftChild, value);
                } 
                else if (current.IsTwoNode && current.LeftKey.CompareTo(value) < 0)
                {
                    restructuredNode = this.InternalInsert(current.RightChild, value);
                }
                else if(current.IsThreeNode && current.LeftKey.CompareTo(value) > 0)
                {
                    restructuredNode = this.InternalInsert(current.LeftChild, value);
                }
                else if(current.IsThreeNode 
                    && current.LeftKey.CompareTo(value) < 0 
                    && current.RightKey.CompareTo(value) > 0)
                {
                    restructuredNode = this.InternalInsert(current.MiddleChild, value);
                }
                else if(current.IsThreeNode && current.RightKey.CompareTo(value) < 0)
                {
                    restructuredNode = this.InternalInsert(current.RightChild, value);
                }

                if(restructuredNode != null)
                {
                    if(current.IsTwoNode)
                    {
                        if(current.LeftKey.CompareTo(restructuredNode.LeftKey) > 0)
                        {
                            current.RightKey = current.LeftKey;
                            current.LeftKey = restructuredNode.LeftKey;

                            current.LeftChild = restructuredNode.LeftChild;
                            current.MiddleChild = restructuredNode.RightChild;
                        }
                        else if(current.LeftKey.CompareTo(restructuredNode.LeftKey) < 0)
                        {
                            current.RightKey = restructuredNode.LeftKey;

                            current.MiddleChild = restructuredNode.LeftChild;
                            current.RightChild = restructuredNode.RightChild;
                        }
                    }
                    else
                    {
                        Node<T> resultNode = null;

                        // RestructuredNode is LeftChild
                        if(current.LeftKey.CompareTo(restructuredNode.LeftKey) > 0)
                        {
                            resultNode = new Node<T>(current.LeftKey);

                            resultNode.LeftChild = new Node<T>(restructuredNode.LeftKey);
                            resultNode.RightChild = new Node<T>(current.RightKey);

                            resultNode.LeftChild.LeftChild = restructuredNode.LeftChild;
                            resultNode.LeftChild.RightChild = restructuredNode.RightChild;

                            resultNode.RightChild.LeftChild = current.MiddleChild;
                            resultNode.RightChild.RightChild = current.RightChild;
                        }
                        // RestructuredNode is MiddleChild
                        else if(current.LeftKey.CompareTo(restructuredNode.LeftKey) < 0 
                            && current.RightKey.CompareTo(restructuredNode.LeftKey) > 0)
                        {
                            resultNode = new Node<T>(restructuredNode.LeftKey);

                            resultNode.LeftChild = new Node<T>(current.LeftKey);
                            resultNode.RightChild = new Node<T>(current.RightKey);

                            resultNode.LeftChild.LeftChild = current.LeftChild;
                            resultNode.LeftChild.RightChild = restructuredNode.LeftChild;

                            resultNode.RightChild.LeftChild = restructuredNode.RightChild;
                            resultNode.RightChild.RightChild = current.RightChild;
                        }
                        // RestructuredNode is RightChild
                        else if (current.RightKey.CompareTo(restructuredNode.LeftKey) < 0)
                        {
                            resultNode = new Node<T>(current.RightKey);

                            resultNode.LeftChild = new Node<T>(current.LeftKey);
                            resultNode.RightChild = new Node<T>(restructuredNode.LeftKey);

                            resultNode.LeftChild.LeftChild = current.LeftChild;
                            resultNode.LeftChild.RightChild = current.MiddleChild;

                            resultNode.RightChild.LeftChild = restructuredNode.LeftChild;
                            resultNode.RightChild.RightChild = restructuredNode.RightChild;
                        }

                        return resultNode;
                    }
                }
            }
            
            return null;
        }

        public Node<T> Root { get; set; }

        public void Insert(T value)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(value);
            }
            else
            {
                Node<T> resultNode = this.InternalInsert(this.Root, value);

                if(resultNode != null)
                {
                    this.Root = resultNode;
                }
            }
        }
    }
}
