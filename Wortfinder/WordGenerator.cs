﻿using System;
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
			if (wordList.Loaded())
			{
				char[,] letters2D = LettersAs2DArray(fieldSize, letters);
				List<Word> allWords = new List<Word>();
				for(int i = 0; i < letters.Length; i++)
                {
					int row = i / fieldSize;
					int column = i % fieldSize;
					Coordinate coordinate = new Coordinate(row, column);
					List<Word> newWords = CheckRecusive("", fieldSize, (char[,])letters2D.Clone(), new List<Coordinate> { coordinate }, 0);
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
		private char[,] LettersAs2DArray(int fieldSize, char[] letters)
		{
			char[,] letters2D = new char[fieldSize, fieldSize];
			for (int row = 0; row < fieldSize; row++)
			{
				for (int column = 0; column < fieldSize; column++)
				{
					letters2D[row, column] = letters[row * fieldSize + column];
				}
			}
			return letters2D;
		}
		public List<Word> CheckRecusive(string word, int size, char[,] letters, List<Coordinate> coordinates, int dictStartIndex)
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
						allWords.Add(new Word(word, new List<Coordinate>(coordinates)));
					}
					
					List<Coordinate> neightbourCoordinates = GetNeighbourCoordinates(currentRow, currentColumn, size);
					foreach(Coordinate coordinate in neightbourCoordinates)
                    {
						List<Coordinate> newCoordinates = new List<Coordinate>(coordinates)	{ coordinate };
						foreach (Word foundWord in CheckRecusive(word, size, (char[,])letters.Clone(), newCoordinates, beginningIndex))
						{
							allWords.Add(foundWord);
						}
					}
				}
			}
			return allWords;
		}

		private List<Coordinate> GetNeighbourCoordinates(int row, int column, int size)
        {
			List<Coordinate> coordinates = new List<Coordinate>();
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