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
        void AddWordToShow(Word word);
        void SetWordsToShow(List<Word> words);
        void ClearWordsToShow();
        void SetCurrentScore(int score);
        void LettersInactive();
        void LettersActive();
        void SetFoundWordsAmount(int amount);
        void SetBestScores(List<Score> scores);
    }
}
