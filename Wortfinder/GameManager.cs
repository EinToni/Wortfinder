using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Wortfinder
{
    class GameManager
    {
        private readonly GameLibrary gameLibrary;
        private readonly GameScore gameScore;
        private readonly GameTimer gameTimer;
        private readonly ScoreManager scoreManager;
        private readonly IMainWindowController mainWindowController;

        private Game activeGame;
        
        public GameManager(IMainWindowController mainWindowController)
        {
            this.mainWindowController = mainWindowController;
            scoreManager = new ScoreManager();
            gameTimer = new GameTimer(mainWindowController.SetTimer, TimerTimeout);
            gameLibrary = new GameLibrary();
            gameScore = new GameScore();
            gameLibrary.LoadGeneratedGames();
            mainWindowController.SetBestScores(scoreManager.GetTopScores(10));
        }

        public bool TryWord(string selectedWord)
        {
            if (activeGame.WordValid(selectedWord))
            {
                gameScore.WordFound(selectedWord);
                mainWindowController.SetFoundWordsAmount(activeGame.FoundWords);
                mainWindowController.SetCurrentScore(gameScore.Score);
                mainWindowController.ShowWord(activeGame.GetWord(selectedWord));
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
                // Start game
                mainWindowController.SetGameField(fieldSize, activeGame.letters);
                gameTimer.StartTimerInSeconds(gameTimeSeconds);
                mainWindowController.LettersActive();
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("There are no games loaded yet.\nIf this is your first time starting the game, please wait a moment.");
                gameLibrary.CheckLoadedGames(fieldSize, 2);
            }
        }
        public void StopGame()
        {
            mainWindowController.LettersInactive();
            
            //mainWindowController.ShowWords()
            int size = activeGame.size;
            int time = activeGame.GameTimeInSeconds;
            int score = gameScore.Score;
            SaveScoreWindow saveScoreWindow = new SaveScoreWindow(scoreManager, score, size, time);
            saveScoreWindow.ShowDialog();
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
