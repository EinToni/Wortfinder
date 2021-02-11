using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class MainWindowController : IMainWindowController
    {
        private readonly MainWindow mainWindow;
         public MainWindowController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void SetGameField(int size, char[] letters) => mainWindow.SetGameField(size, letters);

        public void NewGame(int fieldSize, int gameTime)
        {
            mainWindow.SetGameField(3, new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' });
        }
        public void TryWord(string selectedWord)
        {
            //throw new NotImplementedException();
        }
        public void SetTimer(int timeInSeconds)
        {

        }
    }
}
