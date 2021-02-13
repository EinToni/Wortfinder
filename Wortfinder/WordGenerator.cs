using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Wortfinder
{
	// Class to controll
	internal class WordGenerator
	{
		private readonly IWordList wordList;
		public WordGenerator(IFactory factory)
		{
			this.wordList = factory.GetWordList();
		}

		// Finds all Words in the Grid
		public List<Word> GetAllWords(char[] letters, int fieldSize)
		{
			if (!wordList.Loaded())
            {
				return new List<Word>();
            }
			char[,] letters2D = new char[fieldSize, fieldSize];
			for (int row = 0; row < fieldSize; row++)
			{
				for (int column = 0; column < fieldSize; column++)
				{
					letters2D[row, column] = letters[row* fieldSize + column];
				}
			}
			List<Word> allWords = new List<Word>();
			for (int row = 0; row < fieldSize; row++)
			{
				for (int column = 0; column < fieldSize; column++)
				{
					List<Word> words = CheckRecusive("", (char[,])letters2D.Clone(), new List<Coordinate> { new Coordinate(row, column) }, 0);
					foreach(Word word in words)
					{
						bool wordAlreadyFound = false;
						foreach(Word existingWord in allWords)
						{
							if (existingWord.Name.Equals(word.Name))
							{
								wordAlreadyFound = true;
								break;
							}
						}
						if (!wordAlreadyFound)
						{
							allWords.Add(word);
						}
					}
				}
			}
			return allWords;
		}

		public List<Word> CheckRecusive(string word, char[,] letters, List<Coordinate> coordinates, int dictStartIndex)
		{
			List<Word> allWords = new List<Word>();
			int currentRow = coordinates[^1].Row;
			int currentColumn = coordinates[^1].Column;
			if (letters[currentRow, currentColumn] != '-')
			{
				int columns = letters.GetLength(0);
				int rows = letters.Length / columns;
				
				// Add letter to word
				word += letters[currentRow, currentColumn];
				letters[currentRow, currentColumn] = '-';

				// Check if any word begins with this letter string
				int beginningIndex = wordList.FindBeginningLinear(word, dictStartIndex);
				if (beginningIndex >= 0)
				{
					if (wordList.CheckWord(word, beginningIndex))
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
                                List<Coordinate> newCoordinates = new List<Coordinate>(coordinates)
                                {
                                    new Coordinate(nextRow, nextColumn)
                                };
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