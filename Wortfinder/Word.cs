using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Word
	{
		public string Name { get; }
		public List<Coordinate> Coordinates { get; }
		public bool Found { get; private set; } = false;
		public Word(string name, List<Coordinate> coordinates)
		{
			Name = name;
			Coordinates = new List<Coordinate>(coordinates);
		}

		public Word(Word word)
		{
			Found = word.Found;
			Name = word.Name;
			Coordinates = new List<Coordinate>(word.Coordinates);
		}
		public bool Equals(string word)
        {
			return Name.Equals(word);
        }
		public void GotFound()
        {
			Found = true;
        }
	}
}
