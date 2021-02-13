using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class Factory : IFactory
    {
        public IWordList GetWordList() => new WordList();
        public IScoreWindowController GetScoreWindowController() => new ScoreWindowController();
    }
}
