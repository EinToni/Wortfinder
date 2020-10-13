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
		public int time { get; set; }
		public int fieldSize { get; set; }
		private List<string> foundWords = null;
		private readonly FieldGenerator fieldGenerator;
		private readonly GuessController guessController;
		private readonly DataController dataController;
		private readonly WordFinder wordFinder;
		private readonly GameTimer gameTimer;
		private readonly MainWindow mainWindow;

		public GameController(MainWindow mainW, Grid letterGrid)
		{
			gameTimer		= new GameTimer();
			dataController	= new DataController();
			wordFinder		= new WordFinder(dataController);
			guessController = new GuessController(this, dataController, letterGrid, mainW.OutputWord);
			fieldGenerator	= new FieldGenerator(letterGrid, guessController);
			mainWindow		= mainW;

			fieldGenerator.InitializeField();
			gameTimer.SetDisplayFunc(DisplayTime);
			foundWords = new List<string>();
		}

		public void NewGame()
		{
			fieldGenerator.NewLetters();
			gameTimer.StartTimer();
			wordFinder.FindAllWords();
		}

		public void FoundCorrectWord(string word)
		{
			if (!foundWords.Contains(word))
			{
				foundWords.Add(word);
				AddPoints(word.Length);
			}
		}

		private void AddPoints(int wordLenth)
		{
			score += wordLenth - 3;
			mainWindow.SetPoints(score);
		}

		private bool DisplayTime(string time)
		{
			mainWindow.remainingTime.Text = time;
			return true;
		}

		public void MouseRelease() => guessController.MouseRelease();
	}
}
