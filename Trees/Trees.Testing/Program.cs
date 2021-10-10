using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trees.BinaryTrees;

namespace Trees.Testing
{
    class Program
    {
        static void TopView(Node<int> current, StringBuilder result, bool isLeft, int stepsLeft, int stepsRight, int previousStepsLeft, int previousStepsRight)
        {
            if (current == null)
            {
                return;
            }

            if (!isLeft)
            {
                TopView(current.LeftChild, result, true, stepsLeft + 1, 0, previousStepsLeft, stepsRight);
            }
            else
            {
                TopView(current.LeftChild, result, isLeft, stepsLeft + 1, stepsRight, previousStepsLeft, previousStepsRight);
            }

            if (stepsLeft == 0 && stepsRight == 0)
            {
                isLeft = !isLeft;
            }

            if ((stepsLeft == 0 && stepsRight == 0) || ((isLeft && stepsLeft > previousStepsLeft) || (!isLeft && stepsRight > previousStepsRight)))
            {
                result.Append(current.Value + " ");
            }

            if(isLeft)
            {
                TopView(current.RightChild, result, false, 0, stepsRight + 1, stepsLeft, previousStepsRight);
            }
            else
            {
                TopView(current.RightChild, result, isLeft, stepsLeft, stepsRight + 1, previousStepsLeft, previousStepsRight);
            }
        }

        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>();

            //Console.ReadLine().Split().ToList().ForEach(e => tree.Add(int.Parse(e)));

            tree.Add(1);
            tree.Add(2);
            tree.Add(7);
            tree.Add(10);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5);
            tree.Add(6);
            tree.Add(-1);
            tree.Add(-7);
            tree.Add(-3);
            tree.Add(-4);
            tree.Add(-5);
            tree.Add(-6);

            //tree.InOrder(Console.WriteLine);

            Console.WriteLine("###################################################");

            StringBuilder result = new StringBuilder();
            TopView(tree.Root, result, true, 0, 0, 0, 0);
            System.Console.WriteLine(result.ToString().Trim());
        }
    }
}
