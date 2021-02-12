using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class GameScoreCalculator
	{
		private int gameTime;
		private int gameFieldSize;
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

		internal void SetDifficulty(int gameFieldSize, int gameTimeSeconds)
		{
			this.gameTime = gameTimeSeconds;
			this.gameFieldSize = gameFieldSize;
		}

		private bool WordHasMinimumLength(int length)
		{
			return length - minimumWordLength >= 0;
		}

		private int Points(int wordLength)
		{
			return wordLength - (minimumWordLength - 1);
		}

		private int Bonus()
		{
			int bonus = 0;
			if(gameTime == 60)
			{
				bonus += 2;
			}else if (gameTime == 180)
			{
				bonus += 1;
			}
			if (gameFieldSize == 4)
			{
				bonus += 2;
			}
			else if (gameFieldSize == 5)
			{
				bonus += 1;
			}
			return bonus;
		}
	}
}
