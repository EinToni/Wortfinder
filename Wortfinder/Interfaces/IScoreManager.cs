using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IScoreManager
	{
		List<Score> GetTopScores(int v);
		void NewScore(int score, int size, int time);
	}
}
