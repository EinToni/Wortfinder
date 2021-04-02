using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class ScoreWindowController : IScoreWindowController
    {
        private bool saveScore = false;
        private string playerName = "";
        private readonly IScoreWindow saveScoreWindow;
        public ScoreWindowController(IScoreWindow saveScoreWindow)
		{
            this.saveScoreWindow = saveScoreWindow;
            saveScoreWindow.SetCallback(SetReturn);
        }
        public void NewScoreWindow(int score)
        {
            saveScoreWindow.SetScore(score);
            saveScoreWindow.ShowWindow();
        }
        public string PlayerName()
        {
            return playerName;
        }
        public bool SaveScore()
        {
            return saveScore;
        }
        private bool SetReturn(bool answere, string name)
        {
            saveScore = answere;
            playerName = name;
            return true;
        }
    }
}
