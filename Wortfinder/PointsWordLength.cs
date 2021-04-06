using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class PointsWordLength : IPointFactor
	{
		public int GetPoints(int wordLength, int gameFieldSize, int gameTime)
		{
			return Math.Max(wordLength - 2, 0);
		}
	}
}
