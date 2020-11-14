using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Word
	{
		private string Name { get; }
		private List<int[]> Coordinates { get; }
		public Word(string name, List<int[]> coordinates)
		{
			Name = name;
			Coordinates = coordinates;
		}
	}
}
