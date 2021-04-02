using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Wortfinder
{
    public class GameGenerator
    {
        private readonly LetterGenerator letterGenerator;
        private readonly WordGenerator wordGenerator;
        public GameGenerator(WordGenerator wordGenerator, LetterGenerator letterGenerator)
        {
            this.letterGenerator = letterGenerator;
            this.wordGenerator = wordGenerator;
        }
        public Game NewGame(int fieldSize)
        {
            char[] letters = letterGenerator.GetNewLetters(fieldSize);
            List<Word> findableWords = wordGenerator.GetAllWords(letters, fieldSize);
            Game game = new Game(letters, fieldSize, findableWords);
            if (findableWords.Count <= fieldSize)
            {
                Thread.Sleep(1000);
                return NewGame(fieldSize);
            }
            return game;
        }
    }
}
