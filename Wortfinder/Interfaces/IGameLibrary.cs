using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	public interface IGameLibrary
	{
		void LoadGeneratedGames();
		Game GetGameWithSize(int fieldSize);
		void CheckLoadedGames(int fieldSize, int v);
	}
}
