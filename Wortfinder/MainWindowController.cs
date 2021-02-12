using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class MainWindowController : IMainWindowController
    {
        private readonly MainWindow mainWindow;
        private readonly GameManager gameManager;
        public MainWindowController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.gameManager = new GameManager(this);
        }

        public void SetGameField(int size, char[] letters) => mainWindow.SetGameField(size, letters);
        public void NewGame(int fieldSize, int gameTimeInMinutes) => gameManager.NewGame(fieldSize, gameTimeInMinutes * 60);
        public void TryWord(string selectedWord) => gameManager.TryWord(selectedWord);
        public bool SetTimer(int timeInSeconds)
        {
            mainWindow.Time = timeInSeconds.ToString() + " s";
            //mainWindow.SetTime(timeInSeconds.ToString());
            return true;
        }
        public void ShowWord(Word word) => mainWindow.AddFoundWord(word);
        public void ShowWords(List<Word> words) => mainWindow.ShowWords(words);
        
        internal void StopGame() => gameManager.StopGame();
        internal int GetFieldSize() => gameManager.GetFieldSize();
        public void LettersActive() => mainWindow.LettersActive();
        public void LettersInactive() => mainWindow.LettersInactive();
        public void SetCurrentScore(int score) => mainWindow.ActualScore = score.ToString();
        public void SetMaxWordsFindable(int amount) => mainWindow.FindableWordsAmount = amount.ToString();
        public void SetFoundWordsAmount(int amount) => mainWindow.FoundWordsAmount = amount.ToString();
        public void SetBestScores(List<Score> scores) => mainWindow.SetBestScores(scores);
    }
}
