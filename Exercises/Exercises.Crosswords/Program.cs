using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;

class Result
{
    class Position
    {
        public bool IsDown { get; set; }

        public int StartRow { get; set; }

        public int StartColumn { get; set; }

        public int Length { get; set; }
    }
    /*
     * Complete the 'crosswordPuzzle' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. STRING_ARRAY crossword
     *  2. STRING words
     */

    // either down or right
    // direction // startindex // length

    static List<Position> positions = new List<Position>();

    static List<StringBuilder> crosswordMine = null;

    static bool isSolved = false;

    static int[] GetSectionCoordinates(Position currentPosition)
    {
        foreach (var otherPosition in positions.OrderBy(position => position.StartRow).ThenBy(position => position.StartColumn))
        {
            bool isSame = (otherPosition.StartRow == currentPosition.StartRow && otherPosition.StartColumn == currentPosition.StartColumn);

            bool isBetweenOtherColumns = currentPosition.StartColumn >= otherPosition.StartColumn
                                    && currentPosition.StartColumn <= (otherPosition.StartColumn + otherPosition.Length);
            bool isBetweenMyColumns = otherPosition.StartColumn >= currentPosition.StartColumn
                                    && otherPosition.StartColumn <= (currentPosition.StartColumn + currentPosition.Length);
            bool isBetweenOtherRows = currentPosition.StartRow >= otherPosition.StartRow
                                    && currentPosition.StartRow <= (otherPosition.StartRow + otherPosition.Length);
            bool isBetweenMyRows = otherPosition.StartRow >= currentPosition.StartRow
                                    && otherPosition.StartRow <= (currentPosition.StartRow + currentPosition.Length);

            bool isDownSectioned = isBetweenOtherColumns && isBetweenMyRows;
            bool isRightSectioned = isBetweenOtherRows && isBetweenMyColumns;

            if (!isSame && isDownSectioned)
            {
                return new int[] { otherPosition.StartRow - currentPosition.StartRow, currentPosition.StartColumn - otherPosition.StartColumn, positions.IndexOf(otherPosition) };
            }
            else if (!isSame && isRightSectioned)
            {
                return new int[] { otherPosition.StartColumn - currentPosition.StartColumn, currentPosition.StartRow - otherPosition.StartRow, positions.IndexOf(otherPosition) };
            }
        }

        return null;
    }

    static void PopulatePositions(List<string> crossword)
    {
        for (int column = 0; column < crossword.Count; column++)
        {
            Position position = new Position
            {
                IsDown = true,
                StartRow = 0,
                StartColumn = column,
                Length = 0
            };

            Position positionDown = new Position
            {
                IsDown = false,
                StartRow = column,
                StartColumn = 0,
                Length = 0
            };

            bool hasFoundPosition = false;
            bool hasFoundPositionDown = false;

            for (int row = 0; row < crossword.Count; row++)
            {
                if (crossword[row][column] == '-')
                {
                    if (!hasFoundPosition)
                    {
                        hasFoundPosition = true;
                        position.StartRow = row;
                    }

                    position.Length++;
                }
                else if (hasFoundPosition && position.Length <= 1)
                {
                    position.StartRow = 0;
                    position.Length = 0;
                    hasFoundPosition = false;
                }

                if (crossword[column][row] == '-')
                {
                    if (!hasFoundPositionDown)
                    {
                        hasFoundPositionDown = true;
                        positionDown.StartColumn = row;
                    }

                    positionDown.Length++;
                }
                else if (hasFoundPositionDown && positionDown.Length <= 1)
                {
                    positionDown.StartColumn = 0;
                    positionDown.Length = 0;
                    hasFoundPositionDown = false;
                }
            }

            if (position.Length > 1)
            {
                positions.Add(position);
            }

            if (positionDown.Length > 1)
            {
                positions.Add(positionDown);
            }
        }

        positions = positions.OrderBy(position => position.StartRow).ThenBy(position => position.StartColumn).ToList();
    }

    static void Solve(int index, int totalWords, Dictionary<int, List<string>> wordsByLength, List<string> currentSolution)
    {
        if (isSolved) return;

        if (index >= totalWords)
        {
            if (currentSolution.Count == totalWords)
            {
                isSolved = true;

                for (int i = 0; i < currentSolution.Count; i++)
                {
                    Position currentPosition = positions[i];
                    string currentWord = currentSolution[i];

                    if(currentPosition.IsDown)
                    {
                        for (int row = currentPosition.StartRow; row < currentPosition.StartRow + currentPosition.Length; row++)
                        {
                            crosswordMine[row][currentPosition.StartColumn] = currentWord[row - currentPosition.StartRow];
                        }
                    }
                    else
                    {
                        for (int column = currentPosition.StartColumn; column < currentPosition.StartColumn + currentPosition.Length; column++)
                        {
                            crosswordMine[currentPosition.StartRow][column] = currentWord[column - currentPosition.StartColumn];
                        }
                    }
                }

                crosswordMine.ForEach(x => Console.WriteLine(x));
            }
        }
        else
        {
            for (int i = index; i < positions.Count && !isSolved; i++)
            {
                Position currentPosition = positions[i];
                int[] sectionCoordinates = GetSectionCoordinates(currentPosition);
                int currentCoordinates = sectionCoordinates[0];
                int otherCoordinates = sectionCoordinates[1];
                int otherPositionIndex = sectionCoordinates[2];
                string otherWord = currentSolution.Count == 0 ? null : currentSolution[otherPositionIndex];
                
                if (wordsByLength.ContainsKey(currentPosition.Length))
                {
                    foreach (var lengthedWord in wordsByLength[currentPosition.Length])
                    {
                        // TODO: CHECK IF START LETTER MATCHES LETTER FROM PREVIOUS WORD
                        if (currentSolution.Count == 0 || (sectionCoordinates != null && lengthedWord[currentCoordinates] == otherWord[otherCoordinates]))
                        {
                            currentSolution.Add(lengthedWord);
                            Solve(index + 1, totalWords, wordsByLength, currentSolution);
                            currentSolution.RemoveAt(currentSolution.Count - 1);
                        }
                    }
                }
            }
        }
    }

    public static List<string> crosswordPuzzle(List<string> crossword, string words)
    {
        List<string> splittedWords = words.Split(';').ToList();
        Dictionary<int, List<string>> wordsByLength = new Dictionary<int, List<string>>();

        splittedWords.ForEach(word =>
        {
            if (!wordsByLength.ContainsKey(word.Length))
            {
                wordsByLength[word.Length] = new List<string>();
            }

            wordsByLength[word.Length].Add(word);
        });

        PopulatePositions(crossword);
        crosswordMine = crossword.Select(word => new StringBuilder(word)).ToList();

        Solve(0, splittedWords.Count, wordsByLength, new List<string>());

        return crosswordMine.Select(x => x.ToString()).ToList();
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        List<string> crossword = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            string crosswordItem = Console.ReadLine();
            crossword.Add(crosswordItem);
        }

        string words = Console.ReadLine();

        List<string> result = Result.crosswordPuzzle(crossword, words);
    }
}
