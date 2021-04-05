using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Wortfinder.Interfaces;

namespace Wortfinder
{
    public class GameManager : IGameManager
    {
        private readonly IGameLibrary gameLibrary;
        private readonly IGameScore gameScore;
        private readonly IGameTimer gameTimer;
        private readonly IScoreManager scoreManager;
        private readonly IMainWindowController mainWindowController;

        private Game activeGame;
        public bool GameRunning { get; private set; } = false;
        
        public GameManager(IMainWindowController mainWindowController, IScoreManager scoreManager, IGameLibrary gameLibrary, IGameScore gameScore, IGameTimer gameTimer)
        {
            this.mainWindowController   = mainWindowController;
            this.scoreManager           = scoreManager;
            this.gameTimer              = gameTimer;
            this.gameLibrary            = gameLibrary;
            this.gameScore              = gameScore;

            gameTimer.SetTickCallback(mainWindowController.SetTimer);
            gameTimer.SetTimeoutFunc(TimerTimeout);
            gameLibrary.LoadGeneratedGames();
            mainWindowController.SetBestScores(scoreManager.GetTopScores(10));
        }

        public bool TryWord(string selectedWord)
        {
            if (activeGame != null && activeGame.WordValid(selectedWord))
            {
                gameScore.WordFound(selectedWord);
                mainWindowController.SetFoundWordsAmount(activeGame.FoundWords);
                mainWindowController.SetCurrentScore(gameScore.GetScore());
                mainWindowController.AddWordToShow(activeGame.GetWord(selectedWord));
                return true;
            }
            return false;
        }

        public void NewGame(int fieldSize, int gameTimeSeconds)
        {
            try {
                LoadNewGame(fieldSize, gameTimeSeconds);
                ResetPreviusData(gameTimeSeconds);
                StartNewGame(fieldSize, gameTimeSeconds);
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("There are no games loaded yet.\nIf this is your first time starting the game, please wait a moment.");
                gameLibrary.CheckLoadedGames(fieldSize, 1);
            }
        }

        internal void LoadNewGame(int fieldSize, int gameTimeSeconds)
		{
            activeGame = gameLibrary.GetGameWithSize(fieldSize);
            activeGame.SetTime(gameTimeSeconds);
            gameScore.SetDifficulty(fieldSize, gameTimeSeconds);
        }

        internal void ResetPreviusData(int gameTimeSeconds)
		{
            gameScore.ResetScore();
            mainWindowController.SetFoundWordsAmount(0);
            mainWindowController.SetCurrentScore(0);
            mainWindowController.SetTimer(gameTimeSeconds);
            mainWindowController.SetMaxWordsFindable(activeGame.findableWords.Count);
            mainWindowController.ClearWordsToShow();
        }

        internal void StartNewGame(int fieldSize, int gameTimeSeconds)
		{
            mainWindowController.SetGameField(fieldSize, activeGame.letters);
            gameTimer.StartTimerInSeconds(gameTimeSeconds);
            mainWindowController.LettersActive();
            GameRunning = true;
        }

        public void StopGame()
        {
            GameRunning = false;
            mainWindowController.LettersInactive();
            if (activeGame != null) {
                mainWindowController.SetWordsToShow(activeGame.findableWords);
                int size = activeGame.size;
                int time = activeGame.GameTimeInSeconds;
                int score = gameScore.GetScore();
                scoreManager.NewScore(score, size, time);
            }
            mainWindowController.SetBestScores(scoreManager.GetTopScores(10));
        }
        internal bool TimerTimeout()
        {
            StopGame();
            return true;
        }
		public int GetFieldSize() => activeGame.size;
	}
}
