using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class ScoreWindowController : IScoreWindowController
    {
        private bool saveScore = false;
        private string playerName = "";
        public void NewScoreWindow(int score)
        {
            SaveScoreWindow saveScoreWindow = new SaveScoreWindow(SetReturn, score.ToString());
            saveScoreWindow.ShowDialog();
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
