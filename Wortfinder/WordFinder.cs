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
		public List<string> FindAllWords(char[,] letters)
		{
			List<string> allWords = CheckRecusive("", letters, 0, 0, 0);
			return allWords;
		}

		public List<string> CheckRecusive(string wordString, char[,] letters, int currentRow, int currentColumn, int dictStartIndex)
		{
			List<string> allWords = new List<string>();
			if (letters[currentRow, currentColumn] != '-')
			{
				int rows = letters.Rank;
				int columns = letters.GetLength(0);

				// Add letter to word
				wordString += letters[currentRow, currentColumn];
				letters[currentRow, currentColumn] = '-';

				// Check if any word begins with this letter string
				if (dataController.CheckBeginning(wordString, 0))
				{
					if (dataController.CheckWord(wordString, 0))
					{
						allWords.Add(wordString);
					}
					// Add all attached letters and repeat
					for (int i = -1; i <= 1; i++)
					{
						for (int j = -1; j <= 1; j++)
						{
							int nextRow = currentRow + i;
							int nextColumn = currentColumn + j;
							// If cell is in bound
							if (nextRow <= rows && nextRow >= 0 && nextColumn < columns && nextColumn >= 0)
							{
								foreach (string foundWord in CheckRecusive(wordString, letters, nextRow, nextColumn, 0))
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