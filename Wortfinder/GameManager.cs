using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Wortfinder
{
    public class GameManager
    {
        private readonly GameLibrary gameLibrary;
        private readonly GameScore gameScore;
        private readonly GameTimer gameTimer;
        private readonly ScoreManager scoreManager;
        private readonly IMainWindowController mainWindowController;

        private Game activeGame;
        public bool GameRunning { get; private set; } = false;
        
        public GameManager(IMainWindowController mainWindowController, ScoreManager scoreManager, GameLibrary gameLibrary, GameScore gameScore, GameTimer gameTimer)
        {
            this.mainWindowController = mainWindowController;
            this.scoreManager = scoreManager;
            this.gameTimer = gameTimer;
            this.gameLibrary = gameLibrary;
            this.gameScore = gameScore;

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
                mainWindowController.SetCurrentScore(gameScore.Score);
                mainWindowController.AddWordToShow(activeGame.GetWord(selectedWord));
                return true;
            }
            return false;
        }
        public void NewGame(int fieldSize, int gameTimeSeconds)
        {
            try { 
                // Get new game
                activeGame = gameLibrary.GetGameWithSize(fieldSize);
                activeGame.SetTime(gameTimeSeconds);
                gameScore.SetDifficulty(fieldSize, gameTimeSeconds);
                // Reset previus data
                gameScore.ResetScore();
                mainWindowController.SetFoundWordsAmount(0);
                mainWindowController.SetCurrentScore(0);
                mainWindowController.SetTimer(gameTimeSeconds);
                mainWindowController.SetMaxWordsFindable(activeGame.findableWords.Count);
                mainWindowController.ClearWordsToShow();
                // Start game
                mainWindowController.SetGameField(fieldSize, activeGame.letters);
                gameTimer.StartTimerInSeconds(gameTimeSeconds);
                mainWindowController.LettersActive();
                GameRunning = true;
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("There are no games loaded yet.\nIf this is your first time starting the game, please wait a moment.");
                gameLibrary.CheckLoadedGames(fieldSize, 1);
            }
        }
        public void StopGame()
        {
            GameRunning = false;
            mainWindowController.LettersInactive();

            mainWindowController.SetWordsToShow(activeGame.findableWords);
            int size = activeGame.size;
            int time = activeGame.GameTimeInSeconds;
            int score = gameScore.Score;
            scoreManager.NewScore(score, size, time);
            mainWindowController.SetBestScores(scoreManager.GetTopScores(10));
        }
        private bool TimerTimeout()
        {
            StopGame();
            return true;
        }
        internal int GetFieldSize()
        {
            return activeGame.size;
        }
    }
}
