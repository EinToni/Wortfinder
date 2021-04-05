using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IGameGenerator
	{
		public Game NewGame(int fieldSize);
	}
}
