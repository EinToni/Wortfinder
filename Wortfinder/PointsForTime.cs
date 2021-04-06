using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class PointsForTime : IPointFactor
	{
		private readonly int maxMinsForBonus = 3;

		public int GetPoints(int wordLength, int gameFieldSize, int gameTimeInSeconds)
		{
			if (gameTimeInSeconds == 0) 
			{ 
				return 0; 
			}
			int bonus = maxMinsForBonus - SecondsToRoundedMins(gameTimeInSeconds);
			return Math.Max(bonus, 0);
		}

		internal int SecondsToRoundedMins(int seconds)
		{
			return (int)Math.Floor(seconds / 60.0);
		}
	}
}
