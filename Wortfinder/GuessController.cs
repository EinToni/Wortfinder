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
		private readonly WordGenerator wordFinder;
		private readonly GameScore gameScore;
		private FindableWords findableWords;

		public GuessController(DataController dataCtr, FindableWords findableWords, GameScore gameScore)
		{
			dataController = dataCtr;
			this.gameScore = gameScore;
			wordFinder = new WordGenerator(dataController);
			this.findableWords = findableWords;
		}

		public void LoadAllFindableWords(GameGrid gameGrid)
		{
			findableWordsLoaded = false;
			letters = gameGrid.Letters;
			fieldSize = gameGrid.FieldSize;
			Thread wordFinderThread = new Thread(new ThreadStart(GetAllWords))
			{
				Name = "Word Finder Thread"
			};
			wordFinderThread.Start();
		}

		private void GetAllWords()
		{
			allWords = wordFinder.GetAllWords(letters, fieldSize);
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
				gameScore.WordFound(word.Name);
			}
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
				}
				return wordValid;
			}
			return false;
		}
	}
}
