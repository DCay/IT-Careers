using System;
using System.Collections.Generic;

namespace Algorithms.Greedy
{
    class Program
    {
        static int count = 0;

        static Dictionary<string, string> nextDirections = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            nextDirections.Add("R", "D");
            nextDirections.Add("D", "L");
            nextDirections.Add("L", "U");
            nextDirections.Add("U", "R");

            // 3

            // SRR
            // SRD

            int n = int.Parse(Console.ReadLine());

            if (n <= 1)
            {
                Console.WriteLine("S");
                Console.WriteLine($"Snakes count = 1");
            }
            else if (n == 2)
            {
                Console.WriteLine("SR");
                Console.WriteLine($"Snakes count = 1");
            }
            else
            {
                Solve(
                    1,
                    n,
                    "S",
                    new HashSet<string>(),
                    new List<string>(new string[] { "R", "D", "L", "U" }),
                    new List<string>());

                Console.WriteLine($"Snakes count = {count}");
            }
        }

        static void RotateSnakeAndRegister(List<string> currentCombo, HashSet<string> containedCombos)
        {
            if (containedCombos.Contains(string.Join("", currentCombo)))
            {
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    containedCombos.Add(string.Join("", currentCombo));

                    // Rotate
                    for (int j = 0; j < currentCombo.Count; j++)
                    {
                        currentCombo[j] = nextDirections[currentCombo[j]];
                    }
                }
            }
        }

        static void FlipAndRotateSnakeAndRegister(List<string> currentCombo, HashSet<string> containedCombos)
        {
            // Flip
            List<string> mirrorCombo = new List<string>();

            for (int i = 0; i < currentCombo.Count; i++)
            {
                if (currentCombo[i] == "D")
                {
                    mirrorCombo.Add("U");
                }
                else
                {
                    mirrorCombo.Add(currentCombo[i]);
                }
            }

            RotateSnakeAndRegister(currentCombo, containedCombos);
            RotateSnakeAndRegister(mirrorCombo, containedCombos);
        }

        static List<string> GetReversed(List<string> currentCombo)
        {
            List<string> reversedCombo = new List<string>();

            for (int i = currentCombo.Count - 1; i >= 0; i--)
            {
                reversedCombo.Add(currentCombo[i]);
            }

            return reversedCombo;
        }

        static void Solve(
            int index,
            int positions,
            string previous,
            HashSet<string> containedCombos,
            List<string> set,
            List<string> currentCombo)
        {
            if (index >= positions)
            {
                if (!containedCombos.Contains(string.Join("", currentCombo)))
                {
                    Console.WriteLine("S" + string.Join("", currentCombo));
                    FlipAndRotateSnakeAndRegister(currentCombo, containedCombos);
                    FlipAndRotateSnakeAndRegister(GetReversed(currentCombo), containedCombos);
                    count++;
                }
            }
            else
            {
                for (int i = 0; i < set.Count; i++)
                {
                    if (previous == "S"
                        || (previous == "R" && set[i] != "L")
                        || (previous == "D" && set[i] != "U")
                        || (previous == "L" && set[i] != "R")
                        || (previous == "U" && set[i] != "D"))
                    {
                        currentCombo.Add(set[i]);
                        Solve(index + 1, positions, set[i], containedCombos, set, currentCombo);
                        currentCombo.RemoveAt(currentCombo.Count - 1);
                    }
                }
            }
        }
    }
}