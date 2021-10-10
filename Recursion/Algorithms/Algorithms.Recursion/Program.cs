using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Recursion
{
    class Program
    {
        static string[,] labyrinth =
        {
            { "*", "*", "*", "*", "*", "*", "*", "*", "*", "*" },
            { "A", " ", "*", " ", "*", " ", "*", " ", "*", "*" },
            { "*", " ", " ", " ", "*", " ", "*", " ", "*", "*" },
            { "*", " ", "*", "*", "*", " ", "*", " ", "*", "*" },
            { "*", " ", "*", " ", " ", " ", " ", " ", " ", "*" },
            { "*", " ", "*", " ", "*", " ", "*", "*", " ", "*" },
            { "*", " ", "*", " ", "*", " ", "*", " ", " ", "*" },
            { "*", " ", "*", " ", "*", " ", "*", " ", "*", "*" },
            { "*", " ", " ", " ", "*", " ", " ", " ", " ", "B" },
            { "*", "*", "*", "*", "*", "*", "*", "*", "*", "*" },
        };

        static void Main(string[] args)
        {
            Cell cell = GetStartingCell();

            FindPath(cell, new List<string>(), "START");
        }

        static void PrintLabyrinth()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j] == " ")
                    {
                        Console.Write("-" + " ");
                    }
                    else
                    {
                        Console.Write(labyrinth[i, j] + " ");
                    }
                }

                Console.WriteLine();
            }
        }

        static void FindPath(Cell currentCell, List<string> path, string direction)
        {
            //Console.Clear();
            //PrintLabyrinth();
            if(IsInMatrix(currentCell))
            {
                if (IsExit(currentCell))
                {
                    Console.WriteLine(string.Join(Environment.NewLine, path));
                }
                else if (!IsWall(currentCell) && !IsMarked(currentCell))
                {
                    path.Add(direction);
                    Mark(currentCell);

                    Cell above = currentCell.GetAboveCell();
                    Cell right = currentCell.GetCellToRight();
                    Cell below = currentCell.GetBelowCell();
                    Cell left = currentCell.GetCellToLeft();

                    FindPath(above, path, "UP");
                    FindPath(right, path, "RIGHT");
                    FindPath(below, path, "DOWN");
                    FindPath(left, path, "LEFT");

                    Unmark(currentCell);
                    path.RemoveAt(path.Count - 1);
                }
            }
        }

        static void Mark(Cell cell)
        {
            labyrinth[cell.Row, cell.Column] = "#";
        }

        static void Unmark(Cell cell)
        {
            labyrinth[cell.Row, cell.Column] = " ";
        }

        static bool IsMarked(Cell cell)
        {
            return labyrinth[cell.Row, cell.Column] == "#";
        }

        static bool IsExit(Cell cell)
        {
            return labyrinth[cell.Row, cell.Column] == "B";
        }

        static bool IsWall(Cell cell)
        {
            return labyrinth[cell.Row, cell.Column] == "*";
        }

        static Cell GetStartingCell()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j] == "A")
                    {
                        return new Cell(i, j);
                    }
                }
            }

            throw new ArgumentException("No start...");
        }

        static bool IsInMatrix(Cell cell)
        {
            bool isInRows = cell.Row >= 0 && cell.Row <= labyrinth.GetLength(0);
            bool isInColumns = cell.Column >= 0 && cell.Column <= labyrinth.GetLength(1);

            return isInRows && isInColumns;
        }
    }
}