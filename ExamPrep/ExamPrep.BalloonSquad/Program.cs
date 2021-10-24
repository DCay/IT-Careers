using System;

namespace ExamPrep.BalloonSquad
{
    class Program
    {
        static int[,] matrix;
        static int count = 0;

        static void PrintMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write((matrix[row, col] == 1000 ? "*" : "-") + " ");
                }

                Console.WriteLine();
            }
        }

        static void Recursion(int countOfBalloons, int startRow, int startCol)
        {
            if(countOfBalloons == 0)
            {
                //PrintMatrix();
                //Console.WriteLine();
                count++;
                return;
            }
            else
            {
                for (int row = startRow; row < matrix.GetLength(0); row++)
                {
                    for (int col = startCol; col < matrix.GetLength(1); col++)
                    {
                        if(matrix[row, col] == 0)
                        {
                            // MARKING ROWS
                            for (int i = 0; i < matrix.GetLength(0); i++)
                            {
                                matrix[i, col]++;
                            }

                            // MARKING COLS
                            for (int i = 0; i < matrix.GetLength(1); i++)
                            {
                                matrix[row, i]++;
                            }

                            matrix[row, col] = 1000;

                            Recursion(countOfBalloons - 1, startRow + 1, 0);

                            // UNMARKING ROWS
                            for (int i = 0; i < matrix.GetLength(0); i++)
                            {
                                matrix[i, col]--;
                            }

                            // UNMARKING COLS
                            for (int i = 0; i < matrix.GetLength(1); i++)
                            {
                                matrix[row, i]--;
                            }

                            matrix[row, col] = 0;
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string[] matrixDimensions = Console.ReadLine().Split();
            matrix = new int[int.Parse(matrixDimensions[0]), int.Parse(matrixDimensions[1])];
            int countOfBalloons = int.Parse(Console.ReadLine());

            if(countOfBalloons > matrix.GetLength(0) || countOfBalloons > matrix.GetLength(1))
            {
                Console.WriteLine(0);
                return;
            }

            // 0 0 0
            // 0 0 0
            // 2

            // 1 0 0     // 1 0 0     // 0 1 0
            // 0 1 0     // 0 0 1     // 1 0 0

            // 0 1 0     // 0 0 1     // 0 0 1
            // 0 0 1     // 1 0 0     // 0 1 0


            Recursion(countOfBalloons, 0, 0);
            Console.WriteLine(count);

        }
    }
}
