using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class GameScore : IGameScore
	{
		private int score = 0;
		private readonly IGameScoreCalculator scoreCalculator;

		public GameScore(IGameScoreCalculator scoreCalculator)
		{
			this.scoreCalculator = scoreCalculator;
		}

		public int GetScore()
		{
			return score;
		}

		public void ResetScore()
		{
			score = 0;
		}

		public void AddPoints(int points)
		{
			score += points;
		}

		public void WordFound(string selectedWord)
		{
			AddPoints(scoreCalculator.GetPoints(selectedWord.Length));
		}

		public void SetDifficulty(int fieldSize, int gameTimeSeconds) => scoreCalculator.SetDifficulty(fieldSize, gameTimeSeconds);
	}
}
