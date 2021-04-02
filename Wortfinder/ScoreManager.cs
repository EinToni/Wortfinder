using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Wortfinder
{
	public class ScoreManager
	{
        internal List<Score> scores { private set; get; }
        private readonly IScoreWindowController scoreWindowController;
        private readonly IScoreDataController scoreDataController;

        public ScoreManager(IScoreWindowController scoreWindowController, IScoreDataController scoreDataController)
		{
            this.scoreWindowController = scoreWindowController;
            this.scoreDataController = scoreDataController;
            LoadScores();
        }

        public List<Score> GetTopScores(int amountOfScores)
		{
            if (amountOfScores > scores.Count)
            {
                amountOfScores = scores.Count;
            }
            Score[] scorePart = new Score[amountOfScores];
            scores.CopyTo(0, scorePart, 0, amountOfScores);
            List<Score> scoreList = scorePart.ToList();
            scoreList.Sort();
            return scoreList;
        }

        private void LoadScores()
		{
            scores = scoreDataController.LoadScores();
            scores.Sort();
        }

		internal void SaveScores()
		{
            scoreDataController.SaveScores(scores);
        }

        internal void NewScore(int score, int size, int time)
        {
            scoreWindowController.NewScoreWindow(score);
            if (scoreWindowController.SaveScore())
            {
                AddScore(score, size, time, scoreWindowController.PlayerName());
                SaveScores();
            }
        }

        internal bool AddScore(int score, int fieldSize, int gameTimeInMinutes, string name)
        {
            scores.Add(new Score(score, fieldSize, gameTimeInMinutes, name, DateTime.Now));
            return true;
        }
    }
}
