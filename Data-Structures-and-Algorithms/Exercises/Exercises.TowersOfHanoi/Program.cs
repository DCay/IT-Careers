using System;

namespace Exercises.TowersOfHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] poles = { "A", "B", "C" };

            solveTowers(poles[0], poles[2], poles[1], 5);
        }

        static void solveTowers(string start, string end, string temp, int index)
        {
            if (index == 0)
            {
                return;
            }

            solveTowers(start, temp, end, index - 1);
            System.Console.WriteLine($"Moved disk from {start} to {end}.");
            solveTowers(temp, end, start, index - 1);
        }
    }
}
