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
        public void NewGame(int fieldSize, int gameTime) => gameManager.NewGame(fieldSize, gameTime);
        public void TryWord(string selectedWord) => gameManager.TryWord(selectedWord);
        public bool SetTimer(int timeInSeconds)
        {
            mainWindow.SetTime(timeInSeconds.ToString());
            return true;
        }
        public void ShowWord(Word word) => mainWindow.ShowWord(word);
        public void ShowWords(List<Word> words) => mainWindow.ShowWords(words);
        public void SetScore(int score) => mainWindow.SetScore(score.ToString());
        internal void StopGame() => gameManager.StopGame();
        internal int GetFieldSize() => gameManager.GetFieldSize();
        public void LettersActive() => mainWindow.LettersActive();
        public void LettersInactive() => mainWindow.LettersInactive();
        public void SetMaxWordsFindable(int amount) => mainWindow.SetMaxWords(amount.ToString());
        public void SetFoundWordsAmount(int amount) => mainWindow.SetFoundWordsAmount(amount.ToString());
    }
}
