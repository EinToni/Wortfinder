using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	class LetterGenerator
	{
		public LetterGenerator()
		{

		}

		public char[] GetLetters(int count)
		{
			Random rnd = new Random();
			char[] letters = new char[count];
			for(int i = 0; i < count; i++)
			{

				letters[i] = (char)rnd.Next(65, 90);
			}
			return letters;
		}
	}
}
