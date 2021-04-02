using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IGameScoreCalculator
	{
		public int GetPoints(int wordLength);
		public void SetDifficulty(int gameFieldSize, int gameTimeSeconds);
	}
}
