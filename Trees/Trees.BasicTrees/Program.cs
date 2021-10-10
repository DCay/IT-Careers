using System;

namespace Trees.BasicTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(7);

            tree.Add(7, 19);
            tree.Add(7, 21);
            tree.Add(7, 14);

            tree.Add(19, 1);
            tree.Add(19, 12);
            tree.Add(19, 31);

            tree.Add(14, 23);
            tree.Add(14, 6);

            foreach (var item in tree.OrderDFS())
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            foreach (var item in tree.OrderBFS())
            {
                Console.Write(item + " ");
            }
        }
    }
}
