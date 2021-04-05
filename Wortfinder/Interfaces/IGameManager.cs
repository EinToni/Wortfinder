using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IGameManager
	{
		bool GameRunning { get; }

		bool TryWord(string selectedWord);
		void NewGame(int v1, int v2);
		void StopGame();
		int GetFieldSize();
	}
}
