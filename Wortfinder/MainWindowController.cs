using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
    public class MainWindowController : IMainWindowController
    {
        private readonly IMainWindow mainWindow;
        private readonly IWordMissingController wordMissingController;
        private readonly IWordBuilder wordBuilder;
        private IGameManager gameManager;
        private string wordBuild = "";
        private readonly List<Coordinate> wordCoords = new List<Coordinate>();

        public MainWindowController(IMainWindow mainWindow, IWordMissingController wordMissingController, IWordBuilder wordBuilder) 
        {
            this.mainWindow = mainWindow;
            this.wordMissingController = wordMissingController;
            this.wordBuilder = wordBuilder;
        }
		internal void SetGameManager(IGameManager gameManager)   => this.gameManager = gameManager;

		public bool SetTimer(int timeInSeconds)
        {
            mainWindow.SetTime(timeInSeconds.ToString() + " s");
            return true;
        }
        
        public void SetGameField(int size, char[] letters) => mainWindow.SetGameField(size, letters);
        public void AddWordToShow(Word word)            => mainWindow.AddWordToShow(word);
        public void SetWordsToShow(List<Word> words)    => mainWindow.SetWordsToShow(words);
        public void ClearWordsToShow()                  => mainWindow.ClearWords();
        public void LettersActive()                     => mainWindow.LettersActive();
        public void LettersInactive()                   => mainWindow.LettersInactive();
        public void SetCurrentScore(int score)          => mainWindow.SetCurrentScore(score.ToString());
        public void SetMaxWordsFindable(int amount)     => mainWindow.SetFindableWordsAmount(amount.ToString());
        public void SetFoundWordsAmount(int amount)     => mainWindow.SetFoundWordsAmount(amount.ToString());
        public void SetBestScores(List<Score> scores)   => mainWindow.SetBestScores(scores);

        public void NewGame(string fieldSize, string gameTimeInMinutes) => gameManager.NewGame(int.Parse(fieldSize), int.Parse(gameTimeInMinutes) * 60);
        internal void StopGame()                        => gameManager.StopGame();
        internal int GetFieldSize()                     => gameManager.GetFieldSize();

        internal bool HoverLetter(string letter, string row, string column)
        {
            return wordBuilder.HoverLetter(letter, new Coordinate(int.Parse(row), int.Parse(column)), gameManager.GameRunning);
        }

        internal bool ClickLetter(string letter, string row, string column)
        {
            return wordBuilder.ClickLetter(letter, new Coordinate(int.Parse(row), int.Parse(column)), gameManager.GameRunning);
        }

        internal void ReleaseMouse()
        {
            gameManager.TryWord(wordBuilder.GetWord());
            wordBuilder.Clear();
            mainWindow.DeselectAllLetters();
        }

		internal void WordMissing()
		{
            wordMissingController.Open();
        }
	}
}
