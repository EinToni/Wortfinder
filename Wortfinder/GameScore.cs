using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GameScore
	{
		public int Score { get; private set; } = 0;
		private readonly GameScoreCalculator scoreCalculator;

		public GameScore()
		{
			scoreCalculator = new GameScoreCalculator();
		}
		public void ResetScore()
		{
			Score = 0;
		}

		public void AddPoints(int points)
		{
			Score += points;
		}

		public void WordFound(string wordLength)
		{
			AddPoints(scoreCalculator.GetPoints(wordLength.Length));
		}

		public void SetDifficulty(int fieldSize, int gameTime) => scoreCalculator.SetDifficulty(fieldSize, gameTime);
	}
}
