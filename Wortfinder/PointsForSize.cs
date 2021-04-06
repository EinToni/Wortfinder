using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class PointsForSize : IPointFactor
	{
		private readonly int maxSizeForBonus = 6;
		public int GetPoints(int wordLength, int gameFieldSize, int gameTime)
		{
			if (gameFieldSize == 0) 
			{ 
				return 0; 
			}
			
			int bonus = maxSizeForBonus - gameFieldSize;
			return Math.Max(bonus, 0);
		}
	}
}
