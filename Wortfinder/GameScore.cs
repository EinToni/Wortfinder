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
		public GameScore(Label scoleLabel)
		{
			Score = 0;
			this.scoleLabel = scoleLabel;
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

		public void UpdateScoreGui()
		{
			scoleLabel.Content = Score;
		}
	}
}
