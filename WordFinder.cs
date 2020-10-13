using System.Collections.Generic;
using System.IO;

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

		public bool CheckBeginning(string beginnginWord)
		{
			return true;
		}

		// Finds all Words in the Grid
		public bool FindAllWords(char[,] letters)
		{
			List<string> allwords = new List<string>();
			for (int i = 0; i < letters.GetLength(0); i++)
			{
				for(int j = 0; j <= letters.Rank; j++)
				{
					char[,] newLetters = letters.Clone() as char[,];
					newLetters[i,j] = '-';
					foreach(string word in CheckLetter(letters[i, j].ToString(), newLetters, i, j))
					{
						allwords.Add(word);
					}
				}
			}
			return false;
		}

		public List<string> CheckLetter(string initial, char[,] letters, int row, int column)
		{
			List<string> allWords = new List<string>();
			int rows = letters.Rank;
			int columns = letters.GetLength(0);
			if (dataController.CheckWord(initial))
			{
				allWords.Add(initial);
			}
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					int newRow = row + i;
					int newColumn = column + j;
					if (newRow <= rows && newRow >= 0 && newColumn < columns && newColumn >= 0)
					{
						if (!letters[newRow, newColumn].Equals('-'))
						{
							char[,] newLetters = letters.Clone() as char[,];
							newLetters[newRow, newColumn] = '-';
							foreach (string s in CheckLetter(initial + letters[newRow, newColumn], newLetters, newRow, newColumn))
							{
								allWords.Add(s);
							}
						}
					}
				}
			}
			return allWords;
		}
	}
}