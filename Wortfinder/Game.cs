using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    [Serializable]
    public class Game
    {
        public readonly char[] letters;
        public readonly int size;
        public readonly List<Word> findableWords;
        public int GameTimeInSeconds { get; private set; } = 0;
        public int FoundWords { get; private set; } = 0;

        public Game(char[] letters, int size, List<Word> findableWords)
        {
            this.findableWords = findableWords;
            this.size = size;
            this.letters = letters;
        }

        public bool WordValid(string selectedWord)
        {
            foreach(Word word in findableWords)
            {
                if (word.Equals(selectedWord) && !word.Found)
                {
                    word.GotFound();
                    FoundWords += 1;
                    return true;
                }
            }
            return false;
        }
        public Word GetWord(string wordString)
        {
            foreach (Word word in findableWords)
            {
                if (word.Equals(wordString))
                {
                    return word;
                }
            }
            throw new Exception("Word to get was not available in the game words.");
        }
        public void SetTime(int gameTimeSeconds)
        {
            if (GameTimeInSeconds == 0)
            {
                GameTimeInSeconds = gameTimeSeconds;
            }
        }
    }
}
