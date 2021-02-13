using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    public interface IFactory
    {
        public IWordList GetWordList();
        public IScoreWindowController GetScoreWindowController();
    }
}
