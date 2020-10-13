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
		public int FieldSize { get; set; }
		private List<string> foundWords = null;
		private readonly FieldGenerator fieldGenerator;
		private readonly GuessController guessController;
		private readonly WordFinder wordFinder;
		private readonly GameTimer gameTimer;
		private readonly MainWindow mainWindow;
		private readonly LetterGenerator letterGenerator;

		public GameController(MainWindow mainW, Grid letterGrid)
		{
			gameTimer		= new GameTimer();
			DataController dataController = new DataController();
			wordFinder		= new WordFinder(dataController);
			guessController = new GuessController(this, dataController, letterGrid, mainW.OutputWord);
			fieldGenerator	= new FieldGenerator(letterGrid, guessController);
			letterGenerator = new LetterGenerator();
			mainWindow		= mainW;

			fieldGenerator.InitializeField();
			gameTimer.SetDisplayFunc(DisplayTime);
			foundWords = new List<string>();
		}

		public void NewGame()
		{
			int letterAmount = FieldSize * FieldSize;
			char[] letters = letterGenerator.GetLetters(letterAmount);
			fieldGenerator.NewLetters(letters);
			gameTimer.StartTimer();
			//wordFinder.FindAllWords();
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
