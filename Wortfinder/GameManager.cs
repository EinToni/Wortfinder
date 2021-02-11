using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class GameManager
    {
        //private Game activeGame;
        private readonly GameScore gameScore;
        private readonly GameGrid gameGrid;
        private readonly GameTimer gameTimer;
        private readonly FindableWords findableWords;
        private readonly ScoreManager scoreManager;
        private readonly IMainWindowController mainWindowController;
        public GameManager(IMainWindowController mainWindowController)
        {
            this.mainWindowController = mainWindowController;
        }

        internal void TryWord(string selectedWord)
        {
            throw new NotImplementedException();
        }
        public void NewGame(int fieldSize, int gameTimeSeconds)
        {
            findableWords.ClearAllWords();
            gameScore.ResetScore();
            gameScore.SetDifficulty(fieldSize, gameTimeSeconds);
            gameGrid.NewGrid(fieldSize);
            gameTimer.StartTimerInMinutes(gameTimeSeconds);
        }
        public void StopGame()
        {
            int size = -1;
            int time = -1;
            int score = -1;
            gameGrid.DeactivateAllLetter();
            findableWords.ShowAllWords();
            SaveScoreWindow saveScoreWindow = new SaveScoreWindow(scoreManager, score, size, time);
            saveScoreWindow.ShowDialog();
        }
    }
}
