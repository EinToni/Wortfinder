using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class GameGrid
	{
		public int FieldSize { get; }
		public char[] Letters { get; }
		public GameGrid(int fieldSize, char[] letters)
		{
			FieldSize = fieldSize;
			Letters = letters;
		}

	}
}
