using System;
using System.Collections.Generic;

namespace DynamicProgramming.Knapsack
{
    public struct Item
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int capacity = int.Parse(Console.ReadLine());

            List<Item> items = new List<Item>();

            string inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "end")
            {
                string[] inputParams = inputLine.Split();

                int weight = int.Parse(inputParams[1]);
                int value = int.Parse(inputParams[2]);

                items.Add(new Item
                {
                    Name = inputParams[0],
                    Weight = weight,
                    Value = value
                });
            }


            int[,] memorizationMatrix = new int[items.Count + 1, capacity + 1];
            bool[,] takenMatrix = new bool[items.Count + 1, capacity + 1];

            for (int row = 0; row < memorizationMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < memorizationMatrix.GetLength(1); column++)
                {
                    if (row == 0 || column == 0)
                    {
                        memorizationMatrix[row, column] = 0;
                    }
                    else
                    {
                        int previousItemValue = memorizationMatrix[row - 1, column];
                        int currentItemValue = 0;

                        if (row - 1 >= 0 && column - items[row - 1].Weight >= 0)
                        {
                            currentItemValue = items[row - 1].Value + memorizationMatrix[row - 1, column - items[row - 1].Weight];
                        }

                        if (currentItemValue < previousItemValue)
                        {
                            memorizationMatrix[row, column] = previousItemValue;
                        }
                        else
                        {
                            memorizationMatrix[row, column] = currentItemValue;
                            takenMatrix[row, column] = true;
                        }
                    }
                }
            }

            // Reconstruct Solution
            int currentRow = memorizationMatrix.GetLength(0) - 1;
            int currentColumn = memorizationMatrix.GetLength(1) - 1;

            List<string> resultItems = new List<string>();
            int totalWeight = 0;

            while (true)
            {
                if (currentRow < 0 || currentColumn < 0 || memorizationMatrix[currentRow, currentColumn] == 0)
                {
                    break;
                }

                if (takenMatrix[currentRow, currentColumn])
                {
                    resultItems.Add(items[currentRow - 1].Name);
                    totalWeight += items[currentRow - 1].Weight;
                    currentColumn -= items[currentRow - 1].Weight;
                }

                currentRow--;
            }

            resultItems.Sort();

            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {memorizationMatrix[memorizationMatrix.GetLength(0) - 1, memorizationMatrix.GetLength(1) - 1]}");
            Console.WriteLine(string.Join("\r\n", resultItems).Trim());
        }
    }
}
