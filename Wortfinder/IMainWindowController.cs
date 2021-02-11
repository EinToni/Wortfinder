using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    interface IMainWindowController
    {
        void SetGameField(int size, char[] letters);
        void NewGame(int fieldSize, int gameTime);
        void TryWord(string selectedWord);
        void SetTimer(int timeInSeconds);
    }
}
