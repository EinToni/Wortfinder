using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class GameScoreCalculator
	{
		public int gameTime { get;  private set; }
		public int gameFieldSize { get; private set; }
		private int minimumWordLength = 3;

		public GameScoreCalculator()
		{

		}

		public int GetPoints(int wordLength)
		{
			if (WordHasMinimumLength(wordLength))
			{
				return Points(wordLength) + Bonus();
			}
			return 0;
		}

		public void SetDifficulty(int gameFieldSize, int gameTimeSeconds)
		{
			this.gameTime = gameTimeSeconds;
			this.gameFieldSize = gameFieldSize;
		}

		private bool WordHasMinimumLength(int length)
		{
			return length - minimumWordLength >= 0;
		}

		internal int Points(int wordLength)
		{
			return wordLength - (minimumWordLength - 1);
		}

		internal int Bonus()
		{
			return BonusForTime(gameTime) + BonusForSize(gameFieldSize);
		}

		internal int BonusForTime(int timeInSeconds)
		{
			int maxMinsForBonus = 3;
			int mins = (int)Math.Floor(timeInSeconds / 60.0);
			int bonus = maxMinsForBonus - mins;
			return Math.Max(bonus, 0);
		}

		internal int BonusForSize(int size)
		{
			int maxSizeForBonus = 6;
			int bonus = maxSizeForBonus - size;
			return Math.Max(bonus, 0);
		}
	}
}
