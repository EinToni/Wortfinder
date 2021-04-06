using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	public interface IWordBuilder
	{
		bool HoverLetter(string letter, Coordinate coordinate, bool gameRunning);
		bool ClickLetter(string letter, Coordinate coordinate, bool gameRunning);
		string GetWord();
		void Clear();
	}
}
