using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    interface IMainWindowController
    {
        void SetGameField(int size, char[] letters);
        bool SetTimer(int timeInSeconds);
        void SetMaxWordsFindable(int amount);
        void ShowWord(Word word);
        void ShowWords(List<Word> words);
        void SetScore(int score);
        void LettersInactive();
        void LettersActive();
        void SetFoundWordsAmount(int amount);
    }
}
