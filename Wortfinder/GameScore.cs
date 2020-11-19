using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GameScore
	{
		public int Score { get; private set; }
		private readonly Label scoleLabel;
		private readonly GameScoreCalculator scoreCalculator;

		public GameScore(Label scoleLabel)
		{
			Score = 0;
			this.scoleLabel = scoleLabel;
			scoreCalculator = new GameScoreCalculator();
		}

		public void ResetScore()
		{
			Score = 0;
			UpdateScoreGui();
		}

		public void AddPoints(int points)
		{
			Score += points;
			UpdateScoreGui();
		}

		public void WordFound(int wordLength)
		{
			AddPoints(scoreCalculator.GetPoints(wordLength));
		}

		public void UpdateScoreGui()
		{
			scoleLabel.Content = Score;
		}

		internal void SetDifficulty(int fieldSize, int gameTime) => scoreCalculator.SetDifficulty(fieldSize, gameTime);
	}
}
