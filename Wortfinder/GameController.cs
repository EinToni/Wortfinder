using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	// Controlls the running game
	public class GameController
	{
		private readonly FieldGenerator fieldGenerator;
		private readonly GameTimer gameTimer;
		private readonly MainWindow mainWindow;
		private readonly LetterGenerator letterGenerator;
		private readonly GuessController guessController;
		private readonly GameGridController gameGridController;
		private readonly WordListController WordListController;

		public GameController(MainWindow mainW, Grid letterGrid, GameScore score)
		{
			mainWindow = mainW;
			gameTimer		= new GameTimer();
			DataController dataController = new DataController();
			dataController.LoadGerman();
			guessController = new GuessController(this, dataController, score);
			fieldGenerator	= new FieldGenerator(guessController, letterGrid);
			letterGenerator = new LetterGenerator();
			gameGridController = new GameGridController(letterGrid, dataController, this);
			WordListController = new WordListController();

			gameTimer.SetDisplayFunc(DisplayTime);
			gameTimer.SetTimeoutFunc(EndGame);
		}

		public void NewGame(int fieldSize, int gameTimeInMinutes)
		{
			char[] letters = letterGenerator.GetNewLetters(fieldSize);
			GameGrid gameGrid = new GameGrid(fieldSize, letters);
			guessController.LoadAllFindableWords(gameGrid);
			fieldGenerator.NewGameField(gameGrid);
			gameTimer.StartTimerInMinutes(gameTimeInMinutes);
			mainWindow.ActivateAllLetters();
		}

		public void FoundCorrectWord(string word)
		{
			mainWindow.amountOfFoundWords.Content = int.Parse(mainWindow.amountOfFoundWords.Content.ToString()) + 1;
			foreach (WordDisplay wordDisplay in mainWindow.allWords.Children)
			{
				if (wordDisplay.Word.Name.Equals(word))
				{
					wordDisplay.WordGotFound();
				}
			}
		}

		private bool DisplayTime(string time)
		{
			mainWindow.remainingTime.Text = time;
			return true;
		}

		public void AddWordToList(Word word)
		{
			mainWindow.AddWordToList(word);
		}

		public void AddAllWordsToList(List<Word> allWords)
		{
			mainWindow.AddAllWordsToList(allWords);
		}

		public bool EndGame()
		{
			mainWindow.ShowAllWords();
			mainWindow.DeactivateAllLetters();
			return true;
		}
	}
}
