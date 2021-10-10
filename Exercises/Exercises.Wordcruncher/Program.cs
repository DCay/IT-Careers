using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Wordcruncher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> words = Console.ReadLine().Split(", ").ToList();
            string targetWord = Console.ReadLine();

            Dictionary<int, HashSet<string>> wordsByPosition = new Dictionary<int, HashSet<string>>();
            Dictionary<string, int> wordsWithUsages = new Dictionary<string, int>();

            int currentIndex = 0;
            string currentTargetWord = targetWord;

            foreach (var word in words)
            {
                wordsWithUsages[word] = !wordsWithUsages.ContainsKey(word) ? 1 : wordsWithUsages[word] + 1;
            }

            while (currentIndex < targetWord.Length)
            {
                foreach (var word in words)
                {
                    if(currentTargetWord.StartsWith(word))
                    {
                        if (!wordsByPosition.ContainsKey(currentIndex))
                        {
                            wordsByPosition.Add(currentIndex, new HashSet<string>());
                        }

                        wordsByPosition[currentIndex].Add(word);
                    }
                }

                currentTargetWord = currentTargetWord.Substring(1);
                currentIndex++;
            }

            Solve(0, targetWord, wordsByPosition, wordsWithUsages, new List<string>());
        }

        static void Solve(int index, 
            string targetWord, 
            Dictionary<int, HashSet<string>> wordsByPositions,
            Dictionary<string, int> wordsWithUsages,
            List<string> currentCombination)
        {
            if (index >= targetWord.Length)
            {
                if (string.Join("", currentCombination) == targetWord)
                {
                    Console.WriteLine(string.Join(" ", currentCombination));
                }
            }
            else
            {
                for (int i = index; i < targetWord.Length; i++)
                {
                    if (wordsByPositions.ContainsKey(i))
                    {
                        foreach (var word in wordsByPositions[i])
                        {
                            if(wordsWithUsages[word] > 0)
                            {
                                wordsWithUsages[word]--;
                                currentCombination.Add(word);
                                Solve(i + word.Length, targetWord, wordsByPositions, wordsWithUsages, currentCombination);
                                currentCombination.RemoveAt(currentCombination.Count - 1);
                                wordsWithUsages[word]++;
                            }
                        }
                    }
                }
            }
        }
    }
}
