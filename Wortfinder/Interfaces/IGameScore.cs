using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	public interface IGameScore
	{
		int GetScore();
		void WordFound(string selectedWord);
		void SetDifficulty(int fieldSize, int gameTimeSeconds);
		void ResetScore();
	}
}
