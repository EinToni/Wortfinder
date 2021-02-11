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

        public void NewGame(int fieldSize, int gameTime)
        {
            mainWindow.SetGameField(3, new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' });
        }
        public void TryWord(string selectedWord) => gameManager.TryWord(selectedWord);
        public void SetTimer(int timeInSeconds)
        {
            mainWindow.SetTime(timeInSeconds.ToString());
        }

        internal void StopGame() => gameManager.StopGame();
    }
}
