using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	public interface IPointFactor
	{
		int GetPoints(int wordLength, int gameFieldSize, int gameTime);
	}
}
