using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Wortfinder
{
	// Class to controll
	internal class WordFinder
	{
		private readonly DataController dataController = null;
		public WordFinder(DataController dataCtr)
		{
			dataController = dataCtr;
		}

		// Finds all Words in the Grid
		public List<Word> FindAllWords(char[] letters, int fieldSize)
		{
			char[,] letters2D = new char[fieldSize, fieldSize];
			for (int i = 0; i < fieldSize; i++)
			{
				for (int j = 0; j < fieldSize; j++)
				{
					letters2D[j, i] = letters[i* fieldSize + j];
				}
			}
			List<Word> allWords = new List<Word>();
			for (int row = 0; row < fieldSize; row++)
			{
				for (int column = 0; column < fieldSize; column++)
				{
					List<Word> words = CheckRecusive("", (char[,])letters2D.Clone(), new List<int[]> { new int[] { row, column } }, 0);
					foreach(Word word in words)
					{
						allWords.Add(word);
					}
				}
			}
			return allWords;
		}

		public List<Word> CheckRecusive(string word, char[,] letters, List<int[]> coordinates, int dictStartIndex)
		{
			List<Word> allWords = new List<Word>();
			int currentRow = coordinates[coordinates.Count - 1][0];
			int currentColumn = coordinates[coordinates.Count - 1][1];
			if (letters[currentRow, currentColumn] != '-')
			{
				int columns = letters.GetLength(0);
				int rows = letters.Length / columns;
				
				// Add letter to word
				word += letters[currentRow, currentColumn];
				letters[currentRow, currentColumn] = '-';

				// Check if any word begins with this letter string
				int beginningIndex = dataController.FindBeginningLinear(word, dictStartIndex);
				if (beginningIndex >= 0)
				{
					if (dataController.CheckWord(word, beginningIndex))
					{
						allWords.Add(new Word(word, coordinates));
					}
					// Add all attached letters and repeat
					for (int i = -1; i <= 1; i++)
					{
						for (int j = -1; j <= 1; j++)
						{
							int nextRow = currentRow + i;
							int nextColumn = currentColumn + j;
							// If cell is in bound
							if (nextRow < rows && nextRow >= 0 && nextColumn < columns && nextColumn >= 0)
							{
								List<int[]> newCoordinates = new List<int[]>(coordinates);
								newCoordinates.Add(new int[] { nextRow, nextColumn });
								foreach (Word foundWord in CheckRecusive(word, (char[,])letters.Clone(), newCoordinates, beginningIndex))
								{
									allWords.Add(foundWord);
								}
							}
						}
					}
				}
			}
			return allWords;
		}
	}
}