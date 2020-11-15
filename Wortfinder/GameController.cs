using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	// Controlls the running game
	public class GameController
	{
		private int score = 0;
		public int Time { get; set; }
		public int FieldSize { get; set; } = 4;
		private bool AllWordsSaved { get; set; } = false;
		private readonly List<string> foundWords = null;
		private readonly FieldGenerator fieldGenerator;
		private readonly GameTimer gameTimer;
		private readonly MainWindow mainWindow;
		private readonly LetterGenerator letterGenerator;
		private readonly GuessController guessController;

		public GameController(MainWindow mainW, Grid letterGrid)
		{
			mainWindow = mainW;
			gameTimer		= new GameTimer();
			DataController dataController = new DataController();
			dataController.LoadGerman();
			guessController = new GuessController(this, dataController, mainWindow.allWords);
			fieldGenerator	= new FieldGenerator(guessController, letterGrid);
			letterGenerator = new LetterGenerator();
			

			fieldGenerator.InitializeField(FieldSize);
			gameTimer.SetDisplayFunc(DisplayTime);
			gameTimer.SetTimeoutFunc(EndGame);
			foundWords = new List<string>();
		}

		public void NewGame(int fieldSize, int gameTimeInMinutes)
		{
			char[] letters = letterGenerator.GetNewLetters(fieldSize);
			guessController.LoadAllFindableWords(letters, fieldSize);
			fieldGenerator.NewGameField(fieldSize, letters);
			gameTimer.StartTimerInMinutes(gameTimeInMinutes);
			mainWindow.ActivateAllLetters();
		}

		public void FoundCorrectWord(string word)
		{
			AddPoints(word.Length);
			mainWindow.amountOfFoundWords.Content = int.Parse(mainWindow.amountOfFoundWords.Content.ToString()) + 1;
			foreach (WordDisplay wordDisplay in mainWindow.allWords.Children)
			{
				if (wordDisplay.Word.Name.Equals(word))
				{
					wordDisplay.WordGotFound();
				}
			}
		}

		private void AddPoints(int wordLenth)
		{
			score += wordLenth - 2;
			mainWindow.SetPoints(score);
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
