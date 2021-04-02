using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Wortfinder
{
	// Class to controll
	public class WordGenerator
	{
		private readonly IWordList wordList;
		public WordGenerator(WordList wordList)
		{
			this.wordList = wordList;
		}

		public List<Word> GetAllWords(char[] letters, int fieldSize)
		{
			if (wordList.Loaded())
			{
				List<Word> allWords = new List<Word>();
				for(int i = 0; i < letters.Length; i++)
                {
					int row = i / fieldSize;
					int column = i % fieldSize;
					List<Coordinate> coordList = new List<Coordinate> { new Coordinate(row, column) };
					List<Word> newWords = FindWordsRecusive("", fieldSize, (char[])letters.Clone(), coordList, 0);
					AddWords(newWords, allWords);
				}
				return allWords;
			}
			return new List<Word>();
		}
		private void AddWords(List<Word> newWords, List<Word> allWords)
        {
			foreach (Word word in newWords)
			{
				if (WordNotFound(word, allWords))
                {
					allWords.Add(word);
				}
			}
		}
		private bool WordNotFound(Word newWord, List<Word> allWords)
        {
			foreach (Word existingWord in allWords)
			{
				if (existingWord.Name.Equals(newWord.Name))
				{
					return false;
				}
			}
			return true;
		}
		public List<Word> FindWordsRecusive(string word, int size, char[] letters, List<Coordinate> coordinates, int dictStartIndex)
		{
			List<Word> allWords = new List<Word>();
			int position = coordinates[^1].Row * size + coordinates[^1].Column;
			if (letters[position] != '-')
			{
				word += letters[position];
				letters[position] = '-';
				int beginningIndex = wordList.FindBeginningLinear(word, dictStartIndex);
				if (beginningIndex >= 0)
				{
					if (wordList.CheckWord(word, beginningIndex))
					{
						allWords.Add(new Word(word, new List<Coordinate>(coordinates)));
					}
					List<Coordinate> neightbourCoordinates = GetNeighbourCoordinates(position, size);
					foreach(Coordinate coordinate in neightbourCoordinates)
                    {
						List<Coordinate> newCoordinates = new List<Coordinate>(coordinates)	{ coordinate };
						List<Word> words = FindWordsRecusive(word, size, (char[])letters.Clone(), newCoordinates, beginningIndex);
						AddWords(words, allWords);
					}
				}
			}
			return allWords;
		}

		private List<Coordinate> GetNeighbourCoordinates(int position, int size)
        {
			List<Coordinate> coordinates = new List<Coordinate>();
			int row = position / size;
			int column = position % size;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					int nextRow = row + i;
					int nextColumn = column + j;
					if (IsInBound(nextRow, nextColumn, size))
					{
						coordinates.Add(new Coordinate(nextRow, nextColumn));
					}
				}
			}
			return coordinates;
		}
		private bool IsInBound(int row, int column, int size)
        {
			return row < size && row >= 0 && column < size && column >= 0;
		}
	}
}