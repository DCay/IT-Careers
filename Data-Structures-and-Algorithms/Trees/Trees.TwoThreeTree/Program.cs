using System;

namespace Trees.TwoThreeTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TwoThreeTree<int> tree = new TwoThreeTree<int>();

            tree.Insert(50);
            tree.Insert(30);
            tree.Insert(35);
            tree.Insert(20);
            tree.Insert(10);
            tree.Insert(70);
            tree.Insert(33);
            tree.Insert(40);
        }
    }
}
