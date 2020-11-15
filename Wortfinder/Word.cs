using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Word
	{
		public string Name { get; }
		public List<int[]> Coordinates { get; }
		public Word(string name, List<int[]> coordinates)
		{
			Name = name;
			Coordinates = coordinates;
		}
	}
}
