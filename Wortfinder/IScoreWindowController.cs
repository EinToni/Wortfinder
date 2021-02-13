using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    public interface IScoreWindowController
    {
        public void NewScoreWindow(int score);
        public bool SaveScore();
        public string PlayerName();
    }
}
