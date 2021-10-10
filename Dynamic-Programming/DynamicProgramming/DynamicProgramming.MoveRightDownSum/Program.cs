using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming.MoveRightDownSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                int[] currentInputRow = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = currentInputRow[j];
                }
            }

            int[,] memorizationMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    int leftValue = 0;
                    int upValue = 0;

                    if (column - 1 >= 0)
                    {
                        leftValue = memorizationMatrix[row, column - 1];
                    }

                    if (row - 1 >= 0)
                    {
                        upValue = memorizationMatrix[row - 1, column];
                    }

                    memorizationMatrix[row, column] =
                        matrix[row, column] + Math.Max(leftValue, upValue);
                }
            }

            int currentRow = rows - 1;
            int currentColumn = cols - 1;

            Stack<string> results = new Stack<string>();

            while (true)
            {
                results.Push($"[{currentRow}, {currentColumn}]");

                if (currentColumn == 0 && currentRow == 0)
                {
                    // Print
                    break;
                }

                int upValue = 0;
                int leftValue = 0;

                if (currentRow > 0)
                {
                    upValue = memorizationMatrix[currentRow - 1, currentColumn];
                }

                if (currentColumn > 0)
                {
                    leftValue = memorizationMatrix[currentRow, currentColumn - 1];
                }

                if (upValue > leftValue)
                {
                    currentRow--;
                }
                else
                {
                    currentColumn--;
                }
            }

            Console.WriteLine(string.Join(" ", results));

            //for (int row = 0; row < memorizationMatrix.GetLength(0); row++)
            //{
            //    for (int column = 0; column < memorizationMatrix.GetLength(1); column++)
            //    {
            //        Console.Write(memorizationMatrix[row, column] + " ");
            //    }

            //    Console.WriteLine();
            //}
        }
    }
}
