using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GuessController
	{
		private readonly DataController dataController;
		private char[] letters;
		private int fieldSize;
		private bool findableWordsLoaded = false;
		private List<Word> allWords;
		private readonly WordFinder wordFinder;
		private readonly GameScore gameScore;
		public event EventHandler<Word> FoundCorrectWordEvent;
		private FindableWords findableWords;
		private readonly int minWordLength = 3;

		public GuessController(DataController dataCtr, FindableWords findableWords, GameScore gameScore)
		{
			dataController = dataCtr;
			this.gameScore = gameScore;
			wordFinder = new WordFinder(dataController);
			this.findableWords = findableWords;
		}

		public void LoadAllFindableWords(GameGrid gameGrid)
		{
			letters = gameGrid.Letters;
			fieldSize = gameGrid.FieldSize;
			Thread wordFinderThread = new Thread(new ThreadStart(GetAllWords));
			wordFinderThread.Name = "Word Finder Thread";
			wordFinderThread.Start();
		}

		private void GetAllWords()
		{
			allWords = wordFinder.FindAllWords(letters, fieldSize);
			foreach(Word word in allWords)
			{
				findableWords.AddNewWord(word);
			}
			findableWordsLoaded = true;
		}

		public void TryWord(Word word)
		{
			if (IsWordValid(word))
			{
				findableWords.WordFound(word);
				gameScore.AddPoints(getPoints(word.Name.Length));
				FoundCorrectWordEvent?.Invoke(this, word);
			}
		}

		public int getPoints(int wordLength)
		{
			int points = wordLength - minWordLength;
			if (points < 0)
			{
				return 0;
			}
			return points;
		}

		public bool IsWordValid(Word tryWord)
		{
			if (findableWordsLoaded)
			{
				foreach (Word word in allWords)
				{
					if (word.Name.Equals(tryWord.Name))
					{
						return true;
					}
				}
			}
			else
			{
				bool wordValid = dataController.CheckWord(tryWord.Name, 0);
				if (wordValid)
				{
					findableWords.AddNewWord(tryWord);
					findableWords.WordFound(tryWord);
				}
				return wordValid;
			}
			return false;
		}
	}
}
