using System;

namespace Algorithms.EightQueens
{
    class Program
    {
        static string[,] chessboard =
        {
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0"},
        };

        static int count = 0;

        static void Main(string[] args)
        {
            //MarkAttackedCells(3, 3);
            //PrintMatrix();

            PlaceQueen(0);
            Console.WriteLine($"Solutions: {count}");
        }

        static void PlaceQueen(int row)
        {
            if (row >= 8)
            {
                PrintMatrix();
            }
            else
            {
                // Traverse Columns
                for (int column = 0; column < 8; column++)
                {
                    if (chessboard[row, column] == "0")
                    {
                        MarkAttackedCells(row, column);
                        PlaceQueen(row + 1);
                        UnmarkAttackedCells(row, column);
                    }
                }
            }
        }

        static void MarkAttackedCells(int row, int column)
        {
            for (int i = 0; i < 8; i++)
            {
                chessboard[row, i] = $"{int.Parse(chessboard[row, i]) + 1}";
                chessboard[i, column] = $"{int.Parse(chessboard[i, column]) + 1}";
            }

            int currentRow = row;
            int currentColumn = column;

            while (currentRow >= 1 && currentColumn >= 1)
            {
                chessboard[--currentRow, --currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) + 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow >= 1 && currentColumn < 7)
            {
                chessboard[--currentRow, ++currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) + 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow < 7 && currentColumn < 7)
            {
                chessboard[++currentRow, ++currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) + 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow < 7 && currentColumn >= 1)
            {
                chessboard[++currentRow, --currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) + 1}";
            }

            chessboard[row, column] = "Q";
        }

        static void UnmarkAttackedCells(int row, int column)
        {
            for (int i = 0; i < 8; i++)
            {
                if (chessboard[row, i] == "Q")
                {
                    chessboard[row, i] = "0";
                }

                if (chessboard[i, column] == "Q")
                {
                    chessboard[i, column] = "0";
                }

                chessboard[row, i] = $"{int.Parse(chessboard[row, i]) - 1}";
                chessboard[i, column] = $"{int.Parse(chessboard[i, column]) - 1}";
            }

            int currentRow = row;
            int currentColumn = column;

            while (currentRow >= 1 && currentColumn >= 1)
            {
                chessboard[--currentRow, --currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) - 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow >= 1 && currentColumn < 7)
            {
                chessboard[--currentRow, ++currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) - 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow < 7 && currentColumn < 7)
            {
                chessboard[++currentRow, ++currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) - 1}";
            }

            currentRow = row;
            currentColumn = column;

            while (currentRow < 7 && currentColumn >= 1)
            {
                chessboard[++currentRow, --currentColumn]
                    = $"{int.Parse(chessboard[currentRow, currentColumn]) - 1}";
            }

            chessboard[row, column] = "0";
        }

        static void PrintMatrix()
        {

            Console.WriteLine();

            Console.WriteLine(++count);

            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++) { 
                    if (chessboard[i, j] == "Q")
                    {
                        Console.Write(chessboard[i, j] + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
