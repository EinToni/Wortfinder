using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class GameScoreCalculator : IGameScoreCalculator
	{
		public int GameTime { get;  private set; }
		public int GameFieldSize { get; private set; }
		private readonly int minimumWordLength = 3;
		private readonly List<IPointFactor> pointFactors = new List<IPointFactor>()
		{
			new PointsWordLength(), new PointsForTime(), new PointsForSize()
		};

		public int GetPoints(int wordLength)
		{
			int points = 0;
			if (WordHasMinimumLength(wordLength))
			{
				foreach(IPointFactor pointFactor in pointFactors)
				{
					points += pointFactor.GetPoints(wordLength, GameFieldSize, GameTime);
				}
			}
			return points;
		}

		public void SetDifficulty(int gameFieldSize, int gameTimeSeconds)
		{
			GameTime = gameTimeSeconds;
			GameFieldSize = gameFieldSize;
		}

		internal bool WordHasMinimumLength(int length)
		{
			return length - minimumWordLength >= 0;
		}
	}
}
