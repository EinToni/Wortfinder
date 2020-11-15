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
		private readonly GameController gameController;
		private readonly WrapPanel panel;

		public GuessController(GameController gameCtr, DataController dataCtr, WrapPanel allWords)
		{
			dataController = dataCtr;
			gameController = gameCtr;
			panel = allWords;
			wordFinder = new WordFinder(dataController);
		}

		public void LoadAllFindableWords(char[] letters, int fieldSize)
		{
			this.letters = letters;
			this.fieldSize = fieldSize;
			Thread wordFinderThread = new Thread(new ThreadStart(GetAllWords));
			wordFinderThread.Start();
		}

		private void GetAllWords()
		{
			allWords = wordFinder.FindAllWords(letters, fieldSize);
			gameController.AddAllWordsToList(allWords);
			findableWordsLoaded = true;
		}

		public void TryWord(Word word)
		{
			if (IsWordValid(word))
			{
				gameController.FoundCorrectWord(word.Name);
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
					gameController.AddWordToList(tryWord);
				}
				return wordValid;
			}
			return false;
		}
	}
}
